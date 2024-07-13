using API_Tester.WorldCupDataRepo;
using API_Tester.WorldCupDataRepo.Deserialize;
using System.Text.Json;

namespace API_Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            try
            {
                //WorldCupRepoBroker.getAvailableSavedDataSources();
                bool fatalError = false;
                WorldCupRepoBroker.OnAvailableFileDetailsChanged.Trigger();
                //var fromFile = WorldCupRepoBroker.getRepoFromSavedDataSource(1, ref fatalError);

                //WorldCupRepoBroker.getRepoFromAPI("https://worldcup-vua.nullbit.hr/men", ref fatalError);
            } catch (Exception)
            {
                Console.WriteLine("Borked lol");
            }

            while (true)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Goodbye, World!");
        }
    }
}
