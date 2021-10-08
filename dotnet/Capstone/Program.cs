using Capstone.Classes;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Log that Vending Machine has started
                Logger.Log($"*** Start of Vending Machine Operation: {DateTime.Now} ***");

                //Create a console interface for our vending machine
                ConsoleInterface newConsoleInterface = new ConsoleInterface();

                //Display the main menu
                newConsoleInterface.DisplayMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }
    }
}
