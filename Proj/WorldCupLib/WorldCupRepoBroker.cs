using WorldCupLib.Deserialize;
using TooManyUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupLib
{
    public static class WorldCupRepoBroker
    {
        internal static readonly string baseFilesDir = "CupData";

        internal static readonly string remoteSourceLinks = "/sources.txt";

        internal static readonly string allFilesGroupResultsRelativeLoc = "/group_results.json";
        internal static readonly string allFilesMatchesRelativeLoc = "/matches.json";

        // Once we have an API we check these links to get our data
        internal static readonly string matchesRelativeLink = "/matches";
        internal static readonly string groupResultsRelativeLink = "/teams/group_results";

        internal static readonly Object fileDetailsOperationsLock = new();

        private static SortedSet<AvailableFileDetailsUpdater> availableFileDetailsUpdaters = new();

        public static readonly ThreadLockedEvent OnAvailableFileDetailsChanged = new();
        private static long lastOnAvailableFileDetailsChangedTick = 0;
        public static readonly ThreadLockedEvent OnFatalError = new();

        private static readonly FileSystemMonitor mainDirFSMonitor;

        internal static readonly Object httpClientOperationsLock = new();

        private static IEnumerable<AvailableRemoteSourceDetails>? availableRemoteSources;

        private static readonly HttpClient httpClient = new();
        private static CancellationTokenSource? currentHTTPCTS = null;

        public static readonly ThreadLockedEvent OnAPIStateChanged = new();
        public static readonly ThreadLockedEvent OnAPISourcesLoaded = new();
        public static readonly ThreadLockedEvent OnAPIFailed = new();

        static WorldCupRepoBroker()
        {
            lock (fileDetailsOperationsLock)
            {
                lock (httpClientOperationsLock)
                {
                    ChangeOnMainEventTriggered(); // reentrant locks my beloved
                    mainDirFSMonitor = new(baseFilesDir, true, 100, new(ChangeOnMainEventTriggered), new(CriticalErrorOnMainEventTriggered), new(CriticalErrorOnMainEventTriggered));
                }
            }
        }

        private static void ChangeOnMainEventTriggered()
        {
            EnsureAllFoldersAreMonitored();
            LoadAPISources();
        }
        private static void CriticalErrorOnMainEventTriggered()
        {
            OnFatalError.SafeTrigger();
            mainDirFSMonitor.Dispose();
        }
        
        private static void SubChangedEventTriggered()
        {
            var currTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // We'll want to aggregate these triggers.

            lock (fileDetailsOperationsLock)
            {
                if (currTime <= lastOnAvailableFileDetailsChangedTick)
                    return;
                lastOnAvailableFileDetailsChangedTick = currTime;
            }

            Task.Run(() =>
            {
                Thread.Sleep(2);

                ActOnSubChangedEventTriggered();
            }).ConfigureAwait(false);
        }
        private static void ActOnSubChangedEventTriggered()
        {
            OnAvailableFileDetailsChanged.SafeTrigger();
        }
        private static void SubDisposedEventTriggered(AvailableFileDetailsUpdater updater)
        {
            lock (fileDetailsOperationsLock)
            {
                availableFileDetailsUpdaters.Remove(updater);
            }
            OnAvailableFileDetailsChanged.SafeTrigger();
            EnsureAllFoldersAreMonitored(); // Could have happened due to some sort of error, let's reevaluate.
        }

        private static List<String> GetAllMonitoredFoldersWithFullPath()
        {
            List<String> returnMe = new();

            lock (fileDetailsOperationsLock)
            {
                foreach (var updater in availableFileDetailsUpdaters)
                {
                    string? targetPath = updater.GetTargetPath();
                    if (targetPath == null) continue;

                    returnMe.Add(new DirectoryInfo(targetPath).FullName);
                }
            }

            return returnMe;
        }
        private static void EnsureAllFoldersAreMonitored()
        {
            var subfolders = Directory.GetDirectories(baseFilesDir);

            lock (fileDetailsOperationsLock)
            {
                var monitoredFolders = GetAllMonitoredFoldersWithFullPath();

                foreach (var subfolder in subfolders)
                {
                    string fullSubfolderPath = new DirectoryInfo(subfolder).FullName;
                    if (!monitoredFolders.Contains(fullSubfolderPath))
                    {
                        AvailableFileDetailsUpdater updater = AvailableFileDetailsUpdater.GetInstanceWithoutInit();
                        updater.Init(fullSubfolderPath, new(() => { WorldCupRepoBroker.SubChangedEventTriggered(); }), new(() => { WorldCupRepoBroker.SubDisposedEventTriggered(updater); }));

                        availableFileDetailsUpdaters.Add(updater);
                    }
                }
            }
        }

        private static IEnumerable<AvailableFileDetails> GetAvailableFileDetails()
        {
            LinkedList<AvailableFileDetails> deets = new();
            lock (fileDetailsOperationsLock)
            {
                foreach (var updater in availableFileDetailsUpdaters)
                {
                    deets.AddLast(updater.GetCurrentDetailsCopy());
                }
            }
            return deets;
        }
        public static Task<IEnumerable<AvailableFileDetails>> BeginGetAvailableFileDetails()
        {
            return Task.Run(GetAvailableFileDetails);
        }

        /// <summary>
        /// Gets data and saves it in the standard folder, if you are subscribed to the details changed event you will get auto-notified once the changes are detected. Returns false if already busy.
        /// </summary>
        public static Task<bool> BeginFetchRepoFromAPI(String link, String name = "", int year = 0, String internalImageID = "")
        {
            return Task.Run(() =>
            {
                CancellationToken cancellationToken;

                lock (httpClientOperationsLock)
                {
                    if (!GetAPIFetchIsReady()) return false;

                    CancellationTokenSource cancellationTokenSource = new();
                    cancellationToken = cancellationTokenSource.Token;
                    currentHTTPCTS = cancellationTokenSource;
                    OnAPIStateChanged.SafeTrigger();
                }

                Task.Run(() =>
                {
                    try
                    {
                        Task<String> jsonMatches = httpClient.GetAsync(link + matchesRelativeLink, cancellationToken).Result.Content.ReadAsStringAsync(cancellationToken);
                        Task<String> jsonGroupResults = httpClient.GetAsync(link + groupResultsRelativeLink, cancellationToken).Result.Content.ReadAsStringAsync(cancellationToken);

                        jsonMatches.Wait();
                        jsonGroupResults.Wait();

                        long maxValue = 0;
                        foreach (var deet in GetAvailableFileDetails())
                        {
                            long result = 0;
                            if (Int64.TryParse(deet.ID, out result))
                                if (maxValue < result)
                                    maxValue = result;
                        }

                        String directoryTarget = baseFilesDir + "/" + (maxValue + 1).ToString();

                        Directory.CreateDirectory(directoryTarget);
                        File.WriteAllText(directoryTarget + allFilesGroupResultsRelativeLoc, jsonGroupResults.Result);
                        File.WriteAllText(directoryTarget + allFilesMatchesRelativeLoc, jsonMatches.Result);

                        AvailableFileDetails AFD = new((maxValue + 1).ToString(), name, year, link, internalImageID);
                        AFD.WriteToDirectory(directoryTarget);
                    }
                    catch (Exception)
                    {
                        OnAPIFailed.SafeTrigger();
                    }

                    lock (httpClientOperationsLock)
                    {
                        currentHTTPCTS = null;
                        OnAPIStateChanged.SafeTrigger();
                    }
                }).ConfigureAwait(false);

                return true;
            });
        }
        public static Task<bool> BeginCancelAPIRequest()
        {
            return Task.Run(() =>
            {
                lock (httpClientOperationsLock)
                {
                    if (!GetAPIFetchIsReady()) return false;
                    currentHTTPCTS?.Cancel();
                    currentHTTPCTS = null;

                    return true;
                }
            });
        }
        
        /// <summary>
        /// Returns 1 if currently READY, and 0 otherwise
        /// </summary>
        /// <returns></returns>
        private static bool GetAPIFetchIsReady()
        {
            lock (httpClientOperationsLock)
                return currentHTTPCTS == null;
        }
        public static Task<bool> BeginGetAPIFetchIsReady()
        {
            return Task.Run(GetAPIFetchIsReady);
        }
        private static IEnumerable<AvailableRemoteSourceDetails> GetCurrentAPISources()
        {
            lock (httpClientOperationsLock)
                return availableRemoteSources == null ? new List<AvailableRemoteSourceDetails>() : availableRemoteSources;
        }
        public static Task<IEnumerable<AvailableRemoteSourceDetails>> BeginGetCurrentAPISources()
        {
            return Task.Run(GetCurrentAPISources);
        }
        internal static void LoadAPISources()
        {
            lock (httpClientOperationsLock)
                try
                {
                    availableRemoteSources = AvailableRemoteSourceDetails.GetAllFromFile(baseFilesDir + remoteSourceLinks);
                } catch (Exception){}

            OnAPISourcesLoaded.SafeTrigger();
        }

        public static void BeginRefreshAvailableFiles()
        {
            Task.Run(() =>
            {
                lock (fileDetailsOperationsLock)
                {
                    foreach (var updooter in availableFileDetailsUpdaters)
                    {
                        updooter.Dispose();
                    }
                }
            }).ConfigureAwait(false);
        }

        public static Task<IWorldCupDataRepo?> BeginGetRepoByID(String id)
        {
            return Task.Run(() =>
            {
                bool noErrors = true;
                return GetRepoFromFolder(baseFilesDir + "/" + id, ref noErrors);
            });
        }

        public static bool TryDeleteRepoByID(String id)
        {
            try
            {
                Directory.Delete(baseFilesDir + "/" + id, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static IWorldCupDataRepo? GetRepoFromFolder(String path, ref bool noErrors)
        {
            string? jsonGroupResults;
            string? jsonMatches;

            List<GroupResults> groupResults;
            List<Matches> matches;

            try
            {
                jsonGroupResults = PatientFileAccessor.ReadAllText(path + allFilesGroupResultsRelativeLoc);
                jsonMatches = PatientFileAccessor.ReadAllText(path + allFilesMatchesRelativeLoc);

                if (jsonGroupResults == null || jsonMatches == null)
                    throw new Exception("Ruh roh");

                groupResults = GroupResults.FromJson(jsonGroupResults);
                matches = Matches.FromJson(jsonMatches);
            }
            catch (Exception)
            {
                noErrors = false;
                return null;
            }

            return new WorldCupMemoryRepo(groupResults, matches, ref noErrors);
        }

        /*
        public static IWorldCupDataRepo? FetchRepoFromAPI(String link, ref bool fatalError)
        {
            try
            {
                var jsonMatches = httpClient.GetAsync(link + matchesRelativeLink).Result.Content.ReadAsStringAsync();
                var jsonGroupResults = httpClient.GetAsync(link + groupResultsRelativeLink).Result.Content.ReadAsStringAsync();

                jsonMatches.Wait();
                jsonGroupResults.Wait();

                var matches = Matches.FromJson(jsonMatches.Result);
                var groupResults = GroupResults.FromJson(jsonGroupResults.Result);

                return new WorldCupMemoryRepo(groupResults, matches, ref fatalError);
            }
            catch (Exception e)
            {
                Line(e.Message);
                fatalError = true;
            }
            return null;
        }

        public static IWorldCupDataRepo? GetRepoFromSavedDataSource(int id, ref bool fatalError)
        {
            fatalError = false;

            try
            {
                return GetRepoFromFolder(baseFilesDir + savedFilesRelativeDir + "/" + id, ref fatalError);
            }
            catch (Exception e)
            {
                Line(e.Message);
                fatalError = true;
            }
            return null;
        }

        private static IWorldCupDataRepo? GetRepoFromFolder(String path, ref bool fatalError)
        {
            var jsonGroupResults = File.ReadAllText(path + allFilesGroupResultsRelativeLoc);
            var jsonMatches = File.ReadAllText(path + allFilesMatchesRelativeLoc);

            var groupResults = GroupResults.FromJson(jsonGroupResults);
            var matches = Matches.FromJson(jsonMatches);

            return new WorldCupMemoryRepo(groupResults, matches, ref fatalError);
        }

        public static List<AvailableFolderDetails> GetAvailableSavedDataSources()
        {
            List<AvailableFolderDetails> theReturnables = new();

            string[] dirs;
            try
            {
                dirs = Directory.GetDirectories(baseFilesDir + savedFilesRelativeDir);
            }
            catch (Exception e)
            {
                Line(e.Message);
                return theReturnables;
            }

            for (int i = 0; i < dirs.Length; i++)
            {
                try
                {
                    dirs[i] = dirs[i].Replace("\\", "/");
                    int ID = Int32.Parse(dirs[i].Substring(dirs[i].LastIndexOf('/') + 1));
                    theReturnables.Add(checkFileStructureAndReadDetails(ID, dirs[i]));
                }
                catch (Exception e)
                {
                    Line(e.Message);
                    theReturnables.Add(new(-1, "ERROR", 0, false));
                }
            }

            return theReturnables;
        }

        internal static AvailableFolderDetails checkFileStructureAndReadDetails(int id, String targetPath)
        {
            bool isValid = false;
            if (File.Exists(targetPath + allFilesGroupResultsRelativeLoc) && File.Exists(targetPath + allFilesMatchesRelativeLoc))
                isValid = true;

            try
            {
                var infoFileData = ReadInfoFile(targetPath + savedFilesInfoRelativeLoc);
                return new(id, infoFileData.Key, infoFileData.Value, isValid);
            }
            catch(Exception e)
            {
                Line(e.Message);
            }

            return new(id, "ERROR", 0, isValid);
        }

        internal static KeyValuePair<String, int> ReadInfoFile(String targetPath)
        {
            var infoFile = File.ReadAllLines(targetPath);
            String title = infoFile[0];
            int year = Int32.Parse(infoFile[1]);

            return new(title, year);
        }

        internal static void WriteInfoFile(String targetPath, String Title, int year)
        {
            File.WriteAllLines(targetPath, new string[] { Title, year.ToString() });
        }

        public struct AvailableFolderDetails
        {
            public readonly int ID;
            public readonly String title;
            public readonly int year;
            public readonly bool isValid;

            public AvailableFolderDetails(int ID, string title, int year, bool valid)
            {
                this.ID = ID;
                this.title = title;
                this.year = year;
                this.isValid = valid;
            }
        }*/
    }
}
