using APITester.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITester
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

            string[] data;
            try
            {
                data = PatientFileAccessor.ReadAllLines(dataLocation);
            }
            catch (Exception)
            {
                return returnMe;
            }

            int numSources = data.Length / 3;

            for (int i = 0; i < numSources; i++)
            {
                returnMe.AddLast(new AvailableRemoteSourceDetails(data[i], (Int32.TryParse(data[i + 1], out int tempInt) ? tempInt : 0), data[i + 2]));
            }

            return returnMe;
        }
    }
}
