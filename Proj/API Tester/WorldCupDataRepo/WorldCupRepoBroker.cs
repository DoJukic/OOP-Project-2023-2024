using API_Tester.WorldCupDataRepo.Deserialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace API_Tester.WorldCupDataRepo
{
    public static class WorldCupRepoBroker
    {
        internal static readonly Object operationsLock = new();

        internal static readonly String baseFilesDir = "CupData";

        internal static readonly String remoteSourceLinks = "/sources.txt";

        internal static readonly String allFilesGroupResultsRelativeLoc = "/group_results.json";
        internal static readonly String allFilesMatchesRelativeLoc = "/matches.json";

        // Once we have an API we check these links to get our data
        internal static readonly String matchesRelativeLink = "/matches";
        internal static readonly String groupResultsRelativeLink = "/teams/group_results";

        private static readonly HttpClient httpClient = new();

        private static readonly List<AvailableFileDetailsUpdater> availableFileDetailsUpdaters = new();

        public static readonly ThreadLockedEvent OnAvailableFileDetailsChanged = new();

        private static readonly FileSystemMonitor mainDirFSMonitor;

        static WorldCupRepoBroker()
        {
            mainDirFSMonitor = new(baseFilesDir, true, 100, new(ChangeOnMain), new(ErrorOnMain), new(DeletedTargetFolderOnMain));
        }

        private static void ChangeOnMain()
        {
            Console.WriteLine("CHANGE ON MAIN");
            EnsureAllFoldersAreMonitored();
        }
        private static void ErrorOnMain()
        {
            Console.WriteLine("ERROR ON MAIN");
        }
        private static void DeletedTargetFolderOnMain()
        {
            Console.WriteLine("DELETED TARGET FOLDER ON MAIN");
        }

        private static List<String> GetAllMonitoredFoldersWithFullPath()
        {
            List<String> returnMe = new();

            foreach (var updater in availableFileDetailsUpdaters)
            {
                returnMe.Add(new DirectoryInfo(updater.GetTargetPath()).FullName);
            }

            return returnMe;
        }
        private static void EnsureAllFoldersAreMonitored()
        {
            var subfolders = Directory.GetDirectories(baseFilesDir);

            lock (operationsLock)
            {
                var monitoredFolders = GetAllMonitoredFoldersWithFullPath();

                foreach (var subfolder in subfolders)
                {
                    string fullSubfolderPath = new DirectoryInfo(subfolder).FullName;
                    if (!monitoredFolders.Contains(fullSubfolderPath))
                        monitoredFolders.Add(new(fullSubfolderPath));
                }
            }
        }

        internal static void FileSystemMonitorReportsFailure(AvailableFileDetailsUpdater target)
        {
            lock (operationsLock)
            {
                availableFileDetailsUpdaters.Remove(target);
            }
        }
        public static IEnumerable<AvailableFileDetails> GetAvailableFileDetails()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets data and saves it in the standard folder, if you are subscribed to the details changed event you will get auto-notified once the changes are detected
        /// </summary>
        public static async Task<bool> FetchRepoFromAPIAsync(String link)
        {
            throw new NotImplementedException();
        }

        public static async void RefreshAvailableFilesAsync(String link)
        {
            throw new NotImplementedException();
        }

        public static async void GetRepoByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        internal static IWorldCupDataRepo? GetRepoFromFolder(String path, ref bool fatalError)
        {
            var jsonGroupResults = File.ReadAllText(path + allFilesGroupResultsRelativeLoc);
            var jsonMatches = File.ReadAllText(path + allFilesMatchesRelativeLoc);

            var groupResults = GroupResults.FromJson(jsonGroupResults);
            var matches = Matches.FromJson(jsonMatches);

            return new WorldCupMemoryRepo(groupResults, matches, ref fatalError);
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
                    Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
