using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Tester.WorldCupDataRepo.Interfaces;

namespace API_Tester.WorldCupDataRepo
{
    public static class WorldCupRepoBroker
    {
        private static readonly String baseFilesDir = "data";

        private static readonly String configFileLoc = "config.txt";

        // Temp storage for downloaded API files, can be saved into local for later use. Stomped over when new API is set.
        private static readonly String apiFilesDir = "/downloaded";
        // Contains list of endpoints
        private static readonly String apiFilesDirConfig = "/config.txt";

        // Long storage for downloaded
        private static readonly String localFilesDir = "/local";
        // Contains serialized AvailableFolderDetails (in "data/local/[ID]/info.txt")
        private static readonly String localFilesDirInfo = "/info.txt";

        // Once we have an API we check these links to get our data
        private static readonly String matchesLink = "/matches";
        private static readonly String teamsLink = "/teams";
        private static readonly String resultsLink = "/teams/results";
        private static readonly String groupResultsLink = "/teams/group_results";

        static WorldCupRepoBroker()
        {

        }

        public static IWorldCupDataRepo getRepoFromAPI(String link)
        {

        }

        public static IWorldCupDataRepo getRepoFromFolder(int id)
        {

        }

        public static List<AvailableFolderDetails> getAvailableFiles()
        {

        }

        public class AvailableFolderDetails
        {
            public int? ID { get; }
            public String? Title { get; }
            public int Year { get; }
        }
    }
}
