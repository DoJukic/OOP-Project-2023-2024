using TooManyUtils;
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
        public readonly string internalImageID;

        public AvailableRemoteSourceDetails(string name, int year, string link, string internalImageID)
        {
            this.name = name;
            this.year = year;
            this.link = link;
            this.internalImageID = internalImageID;
        }

        public Task<bool> TryBeginDownload()
        {
            return WorldCupRepoBroker.BeginFetchRepoFromAPI(link, name, year, internalImageID);
        }

        public static IEnumerable<AvailableRemoteSourceDetails> GetAllFromFile(String dataLocation)
        {
            LinkedList<AvailableRemoteSourceDetails> returnMe = new();

            string data;
            try
            {
                var temp = PatientFileAccessor.ReadAllText(dataLocation);

                if (temp == null)
                    throw new Exception("Data is null!");

                data = temp;
            }
            catch (Exception)
            {
                return returnMe;
            }

            var sources = RemoteDataSource.FromJson(data ?? "");

            for (int i = 0; i < sources.Count; i++)
            {
#pragma warning disable CS8629 // Thinks (int)sources[i].Year can be null because I'm checking null with a ternary :(
                returnMe.AddLast(new AvailableRemoteSourceDetails(sources[i].Name, (sources[i].Year != null ? (int)sources[i].Year : 0), sources[i].Link, sources[i].InternalImageID));
#pragma warning restore CS8629
            }

            return returnMe;
        }
    }
}
