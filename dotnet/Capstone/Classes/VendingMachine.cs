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

        public decimal CurrentBalance { get; set; } = 0.0M;

        public decimal TotalSales { get; set; }

        public void Restock(string inputFilePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(inputFilePath))
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
            catch (Exception ex)
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
            string userInput = Console.ReadLine();
            ProcessMainMenuInput(userInput);
        }
        public void ProcessMainMenuInput(string userInput)
        {
            bool validityCheck = int.TryParse(userInput, out int result);
            while (!validityCheck || ( result != 1 && result != 2 && result != 3))
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Enter a numerical menu option displayed above: ");
                userInput = Console.ReadLine();
                validityCheck = int.TryParse(userInput, out result);
            }
            if(result == 1)
            {
                DisplayInventory();
                DisplayMainMenu();
            }
            else if(result == 2)
            {
                DisplayPurchaseMenu();
            }
            else if(result ==3)
            {
                // end program
            }
        }

        public void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("Current Product Inventory");
            foreach(KeyValuePair<string, VendingMachineItem> inv in Inventory)
            {
                string quantity = inv.Value.QuantityRemaining.ToString();
                if(inv.Value.QuantityRemaining == 0)
                {
                    quantity = "Sold Out";
                }
                Console.WriteLine($"Slot#({inv.Value.Slot}) \t{inv.Value.ProductName}: \t${inv.Value.ProductCost}: \tQuantity: {quantity}");
            }
            Console.WriteLine();
            
        }
       
        public void DisplayPurchaseMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Purchase Menu");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine();
            Console.WriteLine($"Current Balance: {CurrentBalance:C}");
            Console.WriteLine();
            Console.WriteLine("Enter a numerical menu option displayed above: ");
            string userInput = Console.ReadLine();
            ProcessPurchaseMenuInput(userInput);
        }

        public void ProcessPurchaseMenuInput(string userInput)
        {
            
            while (userInput != "1" && userInput != "2" && userInput != "3")
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Enter a numerical menu option displayed above: ");
                userInput = Console.ReadLine();
            }
            if (userInput == "1")
            {
                FeedMoney();
            }
            else if (userInput == "2")
            {
                SelectProduct();
            }
            else if (userInput == "3")
            {
                // call finishtransaction
            }
        }

        public void FeedMoney()
        {
            //    -prompt for valid money amounts
            //    - update CurrentBalance and write to console
            //    - check validity of input
            //    -return to / call DisplayPurchaseMenu
            //    - call writeToLog

            Console.WriteLine();
            Console.WriteLine("Please enter a whole dollar amount (no cents) to add to your purchase balance:");
            Console.WriteLine();
            string inputMoney = Console.ReadLine();
            bool isValid = int.TryParse(inputMoney, out int result);
            while(!isValid || result <= 0)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Please enter a whole dollar (no cents) amount:");
                inputMoney = Console.ReadLine();
                isValid = int.TryParse(inputMoney, out result);
            }
            CurrentBalance += result;
            // write to log

            Console.WriteLine();
            DisplayPurchaseMenu();
        }

        public void SelectProduct()
        {
            DisplayInventory();
            Console.WriteLine();
            Console.WriteLine("Enter the code corresponding to the product you want:");
            Console.WriteLine();
            string slot = Console.ReadLine();

            if (!Inventory.ContainsKey(slot))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid code entered.  Please recheck your desired code and try again.");
                DisplayPurchaseMenu();
            }
            else if (Inventory[slot].QuantityRemaining == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Product is sold out.  Sorry!  Please select another product and try again.");
                DisplayPurchaseMenu();
            }
            else if (CurrentBalance < Inventory[slot].ProductCost)
            {
                Console.WriteLine();
                Console.WriteLine("Insufficient funds.  Please feed more money to make this purchase.");
                DisplayPurchaseMenu();
            }
            else
            {
                Inventory[slot].QuantityRemaining--;
                CurrentBalance -= Inventory[slot].ProductCost;
                //write to log
                Console.WriteLine();
                Console.WriteLine("DISPENSING PRODUCT");
                Console.WriteLine($"{Inventory[slot].ProductName}: {Inventory[slot].ProductCost:C}");
                Console.WriteLine(Inventory[slot].PurchaseMessage);
                DisplayPurchaseMenu();
            }
        }
    }
}
