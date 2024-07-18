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
                WorldCupRepoBroker.OnAvailableFileDetailsChanged.SafeSubscribe(PrintReportedState);
                //var fromFile = WorldCupRepoBroker.getRepoFromSavedDataSource(1, ref fatalError);

                //WorldCupRepoBroker.getRepoFromAPI("https://worldcup-vua.nullbit.hr/men", ref fatalError);
            } catch (Exception)
            {
                Console.WriteLine("Borked lol");
            }

            while (true)
            {
                var state = WorldCupRepoBroker.GetAvailableFileDetails();
                Thread.Sleep(1000);
            }

            Console.WriteLine("Goodbye, World!");
        }

        static void PrintReportedState()
        {
            var state = WorldCupRepoBroker.GetAvailableFileDetails();

            String writeMe = "";

            writeMe += "****************************************************\n";
            writeMe += "[" + DateTimeOffset.Now.ToUnixTimeSeconds() + "]: ";
            writeMe += "PRINTING REPORTED STATE....\n";
            foreach (var report in state)
            {
                writeMe += "**************************\n";
                writeMe += "ID: " + report.ID + "\n";
                writeMe += "Name: " + report.Name + "\n";

                writeMe += "Info file valid: " + report.InfoFileValid + "\n";
                writeMe += "File structure valid: " + report.FileStructureValid + "\n";
                writeMe += "JSON valid: " + report.JsonValid + "\n";

                writeMe += "Remote link: " + report.RemoteLink + "\n";
            }
            writeMe += "****************************************************\n";

            Console.Write(writeMe);
        }
    }
}
