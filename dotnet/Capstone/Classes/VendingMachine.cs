using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public Dictionary<string, VendingMachineItem> Inventory { get; set; } = new Dictionary<string, VendingMachineItem>();

        public decimal CurrentBalance { get; set; }

        public decimal TotalSales { get; set; }

        public void Restock(string inputFilePath)
        {
            try
            {
                using(StreamReader sr = new StreamReader(inputFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] itemParts = line.Split("|");
                        string slot = itemParts[0];
                        string productName = itemParts[1];
                        decimal productCost = decimal.Parse(itemParts[2]);
                        string productType = itemParts[3];

                        if (productType == "Chip")
                        {
                            Chips newChips = new Chips(productName, productCost, slot);
                            Inventory[slot] = newChips;
                        }
                        else if (productType == "Candy")
                        {
                            Candy newCandy = new Candy(productName, productCost, slot);
                            Inventory[slot] = newCandy;
                        }
                        else if (productType == "Gum")
                        {
                            Gum newGum = new Gum(productName, productCost, slot);
                            Inventory[slot] = newGum;
                        }
                        else if (productType == "Drink")
                        {
                            Beverage newBeverage = new Beverage(productName, productCost, slot);
                            Inventory[slot] = newBeverage;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred restocking machine..." + ex.Message);
            }
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine("Vendo-Matic 800");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine();
            Console.WriteLine("Enter a numerical menu option displayed above: ");
        }      
    }
}
