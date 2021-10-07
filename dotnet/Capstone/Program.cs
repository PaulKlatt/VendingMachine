using Capstone.Classes;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a Vending Machine instance
            VendingMachine newVendingMachine = new VendingMachine();

            //Create path to inventory file
            string filePath = Path.Combine(Environment.CurrentDirectory, "vendingmachine.csv");

            //Restock
            newVendingMachine.Restock(filePath);

            //Display the main menu
            newVendingMachine.DisplayMainMenu();
        }
    }
}
