using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Tester.WorldCupDataRepo.Utility;

namespace API_Tester.WorldCupDataRepo
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

                var lines = File.ReadAllLines(directoryPath + realtiveFilePath);

                name = lines[0];
                year = Int32.Parse(lines[1]);
                remoteLink = lines[2];
            }
            catch (Exception)
            {
                infoFileValid = false;
            }

            return new(ID, name, year, infoFileValid, fileStructureValid, jsonValid, remoteLink);
        }

        public void WriteToDirectory(string directoryPath)
        {
            String _name = Name;
            String _year = Year.ToString();

            /*
            String _fileStructureValid = FileStructureValid != null ? FileStructureValid.ToString() : "";
            String _jsonValid = JsonValid != null ? JsonValid.ToString() : "";
            */

            String _remoteLink = RemoteLink;

            File.WriteAllLines(directoryPath + realtiveFilePath, new string[] { _name, _year, /*_fileStructureValid, _jsonValid,*/ _remoteLink });
        }
    }

    internal class AvailableFileDetailsUpdater : IDisposable
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
                if (targetPath == null) return;

                this.updateTarget = AvailableFileDetails.ReadFromDirectory(targetPath);
            }
            OnDetailsChanged.SafeTrigger();

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
                OnDisposed.SafeTrigger();
            }
        }

        public string? GetTargetPath()
        {
            return targetPathMonitor != null ? targetPathMonitor.targetPath : null;
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
    }

}
