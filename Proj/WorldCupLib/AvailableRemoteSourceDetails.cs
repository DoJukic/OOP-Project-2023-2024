using WorldCupLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupLib.Deserialize;

namespace WorldCupLib
{
    public class AvailableRemoteSourceDetails
    {
        public readonly string name;
        public readonly int year;
        public readonly string link;

        public AvailableRemoteSourceDetails(string name, int year, string link)
        {
            this.name = name;
            this.year = year;
            this.link = link;
        }

        public Task<bool> TryBeginDownload()
        {
            return WorldCupRepoBroker.BeginFetchRepoFromAPI(link, name, year);
        }

        public static IEnumerable<AvailableRemoteSourceDetails> GetAllFromFile(String dataLocation)
        {
            LinkedList<AvailableRemoteSourceDetails> returnMe = new();

            string data;
            try
            {
                data = PatientFileAccessor.ReadAllText(dataLocation);
            }
            catch (Exception)
            {
                return returnMe;
            }

            var sources = RemoteDataSource.FromJson(data ?? "");

            for (int i = 0; i < sources.Count; i++)
            {
                returnMe.AddLast(new AvailableRemoteSourceDetails(sources[i].Name, (sources[i].Year != null ? (int)sources[i].Year : 0), sources[i].Link));
            }

            return returnMe;
        }
    }
}
