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

                //Create a VendingMachineLogic
                VendingMachineLogic newVendingMachineLogic = new VendingMachineLogic();
                //Create path to inventory file
                string filePath = Path.Combine(Environment.CurrentDirectory, "vendingmachine.csv");
                newVendingMachineLogic.Restock(filePath);

                //Create a console interface for our vending machine
                ConsoleInterface newConsoleInterface = new ConsoleInterface(newVendingMachineLogic);

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
