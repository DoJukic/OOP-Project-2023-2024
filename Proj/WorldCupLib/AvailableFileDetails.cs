using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupLib.Deserialize;
using WorldCupLib.Utility;

namespace WorldCupLib
{
    public class AvailableFileDetails
    {
        const string realtiveFilePath = "/info.txt";

        internal String id;
        internal String name;
        internal int year;

        internal bool infoFileValid;
        internal bool? fileStructureValid;
        internal bool? jsonValid;

        internal String remoteLink;

        public string ID { get => id; }
        public string Name { get => name; }
        public int Year { get => year; }

        public bool InfoFileValid { get => infoFileValid; }
        public bool? FileStructureValid { get => fileStructureValid; }
        public bool? JsonValid { get => jsonValid; }

        public string RemoteLink { get => remoteLink; }

        private AvailableFileDetails(String ID, String? name, int year, bool infoFileValid, bool? fileStructureValid, bool? jsonValid, String? remoteLink)
        {
            this.id = ID;
            this.name = name ?? "ERROR";
            this.year = year;

            this.infoFileValid = infoFileValid;
            this.fileStructureValid = fileStructureValid;
            this.jsonValid = jsonValid;

            this.remoteLink = remoteLink ?? "";
        }
        public AvailableFileDetails(AvailableFileDetails copyTarget)
        {
            this.id = copyTarget.ID;
            this.name = copyTarget.Name ?? "ERROR";
            this.year = copyTarget.Year;

            this.infoFileValid = copyTarget.InfoFileValid;
            this.fileStructureValid = copyTarget.FileStructureValid;
            this.jsonValid = copyTarget.JsonValid;

            this.remoteLink = copyTarget.RemoteLink ?? "";
        }
        public AvailableFileDetails(String ID, String? name, int year, String? remoteLink)
        {
            this.id = ID;
            this.name = name ?? "ERROR";
            this.year = year;

            this.remoteLink = remoteLink ?? "";
        }

        public static AvailableFileDetails ReadFromDirectory(string directoryPath)
        {
            String ID = "ERROR";
            String name = "ERROR";
            int year = 0;
            bool infoFileValid = true;
            bool? fileStructureValid = null;
            bool? jsonValid = null;
            String remoteLink = "ERROR";

            try
            {
                ID = new DirectoryInfo(directoryPath).Name;

                var data = PatientFileAccessor.ReadAllText(directoryPath + realtiveFilePath);
                var info = LocalDataSource.FromJson(data ?? "");

                name = info.Name;
                year = info.Year ?? 0;
                remoteLink = info.RemoteLink;
            }
            catch (Exception)
            {
                infoFileValid = false;
            }

            if (name == null || name == "ERROR" || remoteLink == null || remoteLink == "ERROR")
                infoFileValid = false;

            return new(ID, name, year, infoFileValid, fileStructureValid, jsonValid, remoteLink);
        }

        public void WriteToDirectory(string directoryPath)
        {
            File.WriteAllText(directoryPath + realtiveFilePath, LocalDataSource.ToJson(new() { Name = this.Name, Year = this.Year, RemoteLink = this.RemoteLink}));
        }

        public Task<IWorldCupDataRepo?> BeginGetAssociatedDataRepo()
        {
            return WorldCupRepoBroker.BeginGetRepoByIDAsync(this.ID);
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

        private AvailableFileDetailsUpdater() { }

        /*
        OK SO
        If we are adding a func to an event which we need to listen to immediately and that func requires the reference to this object, we cannot do that because that reference doesn't exist yet.
        Now we can get the object's ref first, and THEN start monitoring the file. ezpz.
        */
        public static AvailableFileDetailsUpdater GetInstanceWithoutInit()
        {
            return new();
        }

        public AvailableFileDetailsUpdater(String targetDirPath, ThreadLockedEvent? OnDetailsChanged = null, ThreadLockedEvent? OnDisposed = null)
        {
            this.Init(targetDirPath, OnDetailsChanged, OnDisposed);
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
