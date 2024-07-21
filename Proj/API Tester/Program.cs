using WorldCupLib;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;

namespace APITester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            try
            {
                WorldCupLib.WorldCupRepoBroker.OnAvailableFileDetailsChanged.Subscribe(PrintReportedState);
                WorldCupLib.WorldCupRepoBroker.OnAPIStateChanged.Subscribe(PrintAPIState);
                PrintAPIState();
            }
            catch (Exception)
            {
                Console.WriteLine("Borked lol");
            }

            Console.WriteLine(WorldCupLib.WorldCupRepoBroker.BeginFetchRepoFromAPI("https://worldcup-vua.nullbit.hr/men", "Name1", 5));
            Console.WriteLine(WorldCupLib.WorldCupRepoBroker.BeginFetchRepoFromAPI("https://worldcup-vua.nullbit.hr/men", "Name1", 5));
            Console.WriteLine(WorldCupLib.WorldCupRepoBroker.BeginFetchRepoFromAPI("https://worldcup-vua.nullbit.hr/men", "Name1", 5));

            while (true)
            {
                var state = WorldCupLib.WorldCupRepoBroker.BeginGetAvailableFileDetails().Result;
                Thread.Sleep(1000);
            }

            Console.WriteLine("Goodbye, World!");
        }

        static void PrintAPIState()
        {
            var state = WorldCupLib.WorldCupRepoBroker.BeginGetAPIFetchIsReady().Result;
            var sources = WorldCupLib.WorldCupRepoBroker.BeginGetCurrentAPISources().Result;

            String writeMe = "";

            writeMe += "####################################################\n";
            writeMe += "[" + DateTimeOffset.Now.ToUnixTimeSeconds() + "]: ";
            writeMe += "API state is " + (state == true ? "READY" : "NOT READY") + "\n";
            writeMe += "SOURCE DETAILS TO FOLLOW...\n";

            foreach (var source in sources)
            {
                writeMe += "##########################\n";

                writeMe += "Name: " + source.name + "\n";
                writeMe += "Year: " + source.year + "\n";
                writeMe += "Link: " + source.link + "\n";

                //writeMe += "Download: " + source.TryBeginDownload() + "\n";
            }

            writeMe += "####################################################\n";

            Console.Write(writeMe);
        }

        static void PrintReportedState()
        {
            var state = WorldCupLib.WorldCupRepoBroker.BeginGetAvailableFileDetails().Result;

            String writeMe = "";

            writeMe += "****************************************************\n";
            writeMe += "[" + DateTimeOffset.Now.ToUnixTimeSeconds() + "]: ";
            writeMe += "PRINTING REPORTED STATE....\n";
            foreach (var report in state)
            {
                /*if (report.ID == "2")
                {
                    var res = report.BeginGetAssociatedDataRepo().Result;
                    res = res;
                }*/

                writeMe += "**************************\n";
                writeMe += "ID: " + report.ID + "\n";
                writeMe += "Name: " + report.Name + "\n";
                writeMe += "Year: " + report.Year + "\n";

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
