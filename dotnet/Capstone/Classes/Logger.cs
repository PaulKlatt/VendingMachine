using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class Logger
    {
        public static string LogFilePath = Path.Combine(Environment.CurrentDirectory, 
            $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}-Transaction Log.txt");

        public static void Log(string logMessage)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(LogFilePath, true))
                {
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file:\n{ex.Message}");
            }
        }
    }
}
