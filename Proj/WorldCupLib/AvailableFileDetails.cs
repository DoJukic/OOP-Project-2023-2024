using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupLib.Deserialize;
using TooManyUtils;
using System.Runtime.CompilerServices;

namespace WorldCupLib
{
    public class AvailableFileDetails
    {
        const string realtiveFilePath = "/info.json";

        internal String id;
        // These are gonna be funny in a thousand years or so when some military hardware gets a dupicate guid and kills a couple billion people
        internal Guid guid;
        internal String name;
        internal int year;

        internal bool infoFileValid;
        internal bool? fileStructureValid;
        internal bool? jsonValid;

        internal String remoteLink;
        private String internalImageID;

        public string ID { get => id; }
        public string GUID { get => guid.ToString(); }
        public string Name { get => name; }
        public int Year { get => year; }

        public bool InfoFileValid { get => infoFileValid; }
        public bool? FileStructureValid { get => fileStructureValid; }
        public bool? JsonValid { get => jsonValid; }

        public string RemoteLink { get => remoteLink; }
        public string InternalImageID { get => internalImageID; }

        private AvailableFileDetails(String ID, Guid guid, String? name, int year, bool infoFileValid, bool? fileStructureValid, bool? jsonValid, String? remoteLink, String? internalImageID)
        {
            this.id = ID;
            this.guid = guid;
            this.name = name ?? "ERROR";
            this.year = year;

            this.infoFileValid = infoFileValid;
            this.fileStructureValid = fileStructureValid;
            this.jsonValid = jsonValid;

            this.remoteLink = remoteLink ?? "";
            this.internalImageID = internalImageID ?? "";
        }
        public AvailableFileDetails(AvailableFileDetails copyTarget)
        {
            this.id = copyTarget.ID;
            this.guid = copyTarget.guid;
            this.name = copyTarget.Name ?? "ERROR";
            this.year = copyTarget.Year;

            this.infoFileValid = copyTarget.InfoFileValid;
            this.fileStructureValid = copyTarget.FileStructureValid;
            this.jsonValid = copyTarget.JsonValid;

            this.remoteLink = copyTarget.RemoteLink ?? "";
            this.internalImageID = copyTarget.InternalImageID ?? "";
        }
        public AvailableFileDetails(String ID, Guid guid, String? name, int year, String? remoteLink, String? internalImageID)
        {
            this.id = ID;
            this.guid= guid;
            this.name = name ?? "ERROR";
            this.year = year;

            this.remoteLink = remoteLink ?? "";
            this.internalImageID = internalImageID ?? "";
        }

        public static AvailableFileDetails ReadFromDirectory(string directoryPath)
        {
            String ID = "ERROR";
            Guid? GUID = null;
            String name = "ERROR";
            int year = 0;
            bool infoFileValid = true;
            bool? fileStructureValid = null;
            bool? jsonValid = null;
            String remoteLink = "ERROR";
            String? InternalImageID = null;

            try
            {
                ID = new DirectoryInfo(directoryPath).Name;

                var data = PatientFileAccessor.ReadAllText(directoryPath + realtiveFilePath);
                var info = LocalDataSource.FromJson(data ?? "");

                GUID = info.GUID;
                name = info.Name;
                year = info.Year ?? 0;
                remoteLink = info.RemoteLink;
                InternalImageID = info.InternalImageID;
            }
            catch (Exception)
            {
                infoFileValid = false;
            }

            if (name == null || name == "ERROR" || remoteLink == null || remoteLink == "ERROR" || InternalImageID == null || InternalImageID == "" ||
                GUID == null || GUID == Guid.Empty)
                infoFileValid = false;

            return new(ID, (Guid)(GUID ?? Guid.Empty), name, year, infoFileValid, fileStructureValid, jsonValid, remoteLink, InternalImageID);
        }

        public void WriteToDirectory(string directoryPath)
        {
            File.WriteAllText(directoryPath + realtiveFilePath, LocalDataSource.ToJson(
                new() { Name = this.Name, GUID = this.guid, Year = this.Year, RemoteLink = this.RemoteLink, InternalImageID = this.InternalImageID}));
        }

        public Task<IWorldCupDataRepo?> BeginGetAssociatedDataRepo()
        {
            return WorldCupRepoBroker.BeginGetRepoByID(this.ID);
        }

        public bool TryDelete()
        {
            return WorldCupRepoBroker.TryDeleteRepoByID(this.ID);
        }
    }

    internal class AvailableFileDetailsUpdater : IDisposable, IComparable<AvailableFileDetailsUpdater>
    {
        // send ms since epoch of request as well so the broker doesn't proces outdated requests?
        private Object suboperationsLock = new();

        private long activeSuboperationID = 0;

        private AvailableFileDetails updateTarget;
        private FileSystemMonitor? targetPathMonitor;

        public ThreadLockedEvent OnDetailsChanged;
        public ThreadLockedEvent OnDisposed;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private AvailableFileDetailsUpdater() { }

        public AvailableFileDetailsUpdater(String targetDirPath, ThreadLockedEvent? OnDetailsChanged = null, ThreadLockedEvent? OnDisposed = null)
        {
            this.Init(targetDirPath, OnDetailsChanged, OnDisposed);
        }
#pragma warning restore CS8618

        /*
        OK SO
        If we are adding a func to an event which we need to listen to immediately and that func requires the reference to this object, we cannot do that because that reference doesn't exist yet.
        Now we can get the object's ref first, and THEN start monitoring the file. ezpz.
        */
        public static AvailableFileDetailsUpdater GetInstanceWithoutInit()
        {
            return new();
        }

        public bool Init(String targetDirPath, ThreadLockedEvent? OnDetailsChanged = null, ThreadLockedEvent? OnDisposed = null)
        {
            lock (suboperationsLock)
            {
                if (targetPathMonitor != null) return false;

                this.OnDetailsChanged = OnDetailsChanged ?? new();
                this.OnDisposed = OnDisposed ?? new();

                try
                {
                    this.updateTarget = AvailableFileDetails.ReadFromDirectory(targetDirPath);
                }
                catch (Exception)
                {
                    this.Dispose();
                }

                targetPathMonitor = new(targetDirPath, false, 100, new(ReevaluateTargetFolder), new(Dispose), new(Dispose));

                return true;
            }
        }

        private void ReevaluateTargetFolder()
        {
            // if a new change was detected after this one was started, we want to make sure we don't write bad data.
            long currentSuboperationID;
            string? targetPath;

            lock (suboperationsLock)
            {
                activeSuboperationID += 1;
                currentSuboperationID = activeSuboperationID;

                targetPath = GetTargetPath();
                if (targetPath == null)
                    return;

                this.updateTarget = AvailableFileDetails.ReadFromDirectory(targetPath);
            }
            OnDetailsChanged.SafeTrigger();

            if (GetCurrentDetailsCopy().ID == "10")
                GetCurrentDetailsCopy();

            if (!File.Exists(targetPath + WorldCupRepoBroker.allFilesGroupResultsRelativeLoc) || !File.Exists(targetPath + WorldCupRepoBroker.allFilesMatchesRelativeLoc))
            {
                lock (suboperationsLock)
                {
                    if (currentSuboperationID != activeSuboperationID)
                        return;

                    this.updateTarget.fileStructureValid = false;
                    this.updateTarget.jsonValid = false;
                }

                OnDetailsChanged.SafeTrigger();
                return;
            }

            lock (suboperationsLock)
            {
                if (currentSuboperationID != activeSuboperationID)
                    return;

                this.updateTarget.fileStructureValid = true;
            }
            OnDetailsChanged.SafeTrigger();

            bool noErrors = true;
            WorldCupRepoBroker.GetRepoFromFolder(targetPath, ref noErrors);

            lock (suboperationsLock)
            {
                if (currentSuboperationID != activeSuboperationID)
                    return;

                this.updateTarget.jsonValid = noErrors;
            }
            OnDetailsChanged.SafeTrigger();
        }

        public void Dispose()
        {
            lock (suboperationsLock)
            {
                activeSuboperationID = -1;
                targetPathMonitor?.Dispose();
                OnDisposed.Trigger();
                OnDetailsChanged.SafeDispose();
                OnDisposed.SafeDispose();
            }
        }

        public string? GetTargetPath()
        {
            lock (suboperationsLock)
            {
                if (targetPathMonitor != null)
                    return targetPathMonitor.targetPath;
                return null;
            }
        }

        public AvailableFileDetails GetCurrentDetailsCopy()
        {
            AvailableFileDetails returnMe;
            lock (suboperationsLock)
            {
                returnMe = new(updateTarget);
            }
            return returnMe;
        }

        public int CompareTo(AvailableFileDetailsUpdater? other)
        {
            int thisInt = 0;
            int otherInt = 0;

            if (other == null)
                return 1;

            if (this.updateTarget == null)
            {
                if (other.updateTarget == null)
                    return 0;
                return -1;
            }
            if (other.updateTarget == null)
                return 1;

            if (!Int32.TryParse(this.updateTarget.id, out thisInt))
            {
                if (!Int32.TryParse(other.updateTarget.id, out otherInt))
                    return this.updateTarget.id.CompareTo(other.updateTarget.id);
                return -1;
            }
            if (!Int32.TryParse(other.updateTarget.id, out otherInt))
                return 1;

            return thisInt.CompareTo(otherInt);
        }
    }

}
