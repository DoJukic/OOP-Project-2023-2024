using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var lines = File.ReadAllLines(directoryPath + realtiveFilePath);

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
                name = lines[0];
                year = Int32.Parse(lines[1]);
                remoteLink = lines[2];

                /*
                if (lines[2] == null || lines[2] == "")
                    infoFileValid = null;
                else
                    infoFileValid = Boolean.Parse(lines[2]);

                if (lines[3] == null || lines[3] == "")
                    fileStructureValid = null;
                else
                    fileStructureValid = Boolean.Parse(lines[3]);

                if (lines[4] == null || lines[4] == "")
                    jsonValid = null;
                else
                    jsonValid = Boolean.Parse(lines[4]);

                remoteLink = lines[5];
                */
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
        private FileSystemMonitor targetPathMonitor;

        public readonly ThreadLockedEvent OnDetailsChanged;
        public readonly ThreadLockedEvent OnDisposed;

        public AvailableFileDetailsUpdater(String targetDirPath, ThreadLockedEvent? OnDetailsChanged = null, ThreadLockedEvent? OnDisposed = null)
        {
            this.OnDetailsChanged = OnDetailsChanged ?? new();
            this.OnDisposed = OnDisposed ?? new();

            try
            {
                this.updateTarget = AvailableFileDetails.ReadFromDirectory(targetDirPath);
            }
            catch(Exception)
            {
                this.Dispose();
            }

            targetPathMonitor = new(targetDirPath, false, 100, new(ReevaluateTargetFolder), new(Dispose), new(Dispose));
        }

        private void ReevaluateTargetFolder()
        {
            // if a new change was detected after this one was started, we want to make sure we don't write bad data.
            long currentSuboperationID;

            lock (suboperationsLock)
            {
                activeSuboperationID += 1;
                currentSuboperationID = activeSuboperationID;
                this.updateTarget = AvailableFileDetails.ReadFromDirectory(GetTargetPath());
            }
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.Trigger();

            if (!File.Exists(GetTargetPath() + WorldCupRepoBroker.allFilesGroupResultsRelativeLoc) || !File.Exists(GetTargetPath() + WorldCupRepoBroker.allFilesMatchesRelativeLoc))
            {
                lock (suboperationsLock)
                {
                    if (currentSuboperationID != activeSuboperationID)
                        return;

                    this.updateTarget.fileStructureValid = false;
                    this.updateTarget.jsonValid = false;
                }

                WorldCupRepoBroker.OnAvailableFileDetailsChanged.Trigger();
                return;
            }

            lock (suboperationsLock)
            {
                if (currentSuboperationID != activeSuboperationID)
                    return;

                this.updateTarget.fileStructureValid = true;
            }
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.Trigger();

            bool fatalErrorWhenReadingJson = false;
            WorldCupRepoBroker.GetRepoFromFolder(GetTargetPath(), ref fatalErrorWhenReadingJson);

            lock (suboperationsLock)
            {
                if (currentSuboperationID != activeSuboperationID)
                    return;

                this.updateTarget.jsonValid = fatalErrorWhenReadingJson;
            }
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.Trigger();
        }

        public void Dispose()
        {
            lock (suboperationsLock)
            {
                activeSuboperationID = -1;
                targetPathMonitor.Dispose();
                OnDisposed.Trigger();
            }
        }

        public string GetTargetPath()
        {
            return targetPathMonitor.targetPath;
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
