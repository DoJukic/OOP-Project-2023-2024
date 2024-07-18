using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Tester.WorldCupDataRepo.Utility
{
    internal static class PatientFileAccessor
    {
        /// <summary>
        /// ATTENTION: this WILL make the thread sleep a bit. You have been warned.
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static String? ReadAllText(string targetPath, int timeout = 100)
        {
            bool success = false;
            String data = ";";

            while (!success)
            {
                timeout--;

                try
                {
                    data = File.ReadAllText(targetPath);
                    success = true;
                }
                catch (IOException)
                {
                    if (timeout <= 0)
                        return null;

                    Console.WriteLine("mimimimimi");
                    Thread.Sleep(1);
                }
            }

            return data;
        }
    }
}
