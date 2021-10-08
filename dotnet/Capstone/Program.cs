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

                //Create a Vending Machine instance
                VendingMachine newVendingMachine = new VendingMachine();

                //Create path to inventory file
                string filePath = Path.Combine(Environment.CurrentDirectory, "vendingmachine.csv");

                //Restock
                newVendingMachine.Restock(filePath);

                //Display the main menu
                newVendingMachine.DisplayMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }
    }
}
