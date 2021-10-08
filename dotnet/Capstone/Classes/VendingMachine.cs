﻿using System;
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
            //Log that we are restocking
            Logger.Log($"*** Restocking the Vending Machine From: {inputFilePath} ***");

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
                //Log that Vending Machine has ended
                Logger.Log($"*** End of Vending Machine Operation: {DateTime.Now} ***");
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
                FinishTransaction();
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

            //Log that money has been feed
            Logger.Log($"{DateTime.Now} FEED MONEY: {result:C} {CurrentBalance:C}");

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
                //Log the product purchased
                Logger.Log($"{DateTime.Now} {Inventory[slot].ProductName} " +
                    $"{Inventory[slot].Slot} {CurrentBalance:C} {(CurrentBalance - Inventory[slot].ProductCost):C}");

                Inventory[slot].QuantityRemaining--;
                CurrentBalance -= Inventory[slot].ProductCost;

                Console.WriteLine();
                Console.WriteLine("DISPENSING PRODUCT");
                Console.WriteLine($"{Inventory[slot].ProductName}: {Inventory[slot].ProductCost:C}");
                Console.WriteLine(Inventory[slot].PurchaseMessage);
                DisplayPurchaseMenu();
            }
        }

        public void FinishTransaction()
        {
            //-converting CurrentBalance to change(in coins, few as possible)
            //   - set CurrentBalance to 0
            //    - displayMainMenu
            //    - call WriteToLog

            //Check that there is money to return
            if (CurrentBalance == 0)
            {
                //Log the change returned
                Logger.Log($"{DateTime.Now} GIVE CHANGE: {CurrentBalance:C} {0:C}");

                DisplayMainMenu();
            }
            else
            {
                //Check if current balance is larger than a quarter
                int quarters = 0;
                int dimes = 0;
                int nickels = 0;
                int pennies = 0;

                decimal remainder = CurrentBalance;

                if (remainder >= 0.25M)
                {
                    //Figure out how many quaters
                    quarters = (int)(remainder / 0.25M);
                    remainder = remainder % 0.25M;
                }

                //Check if reaminder is larger than a dime
                if (remainder >= 0.10M)
                {
                    //Figure out how many dimes
                    dimes = (int)(remainder / 0.10M);
                    remainder = remainder % 0.10M;
                }

                //Check if reaminder is larger than a nickel
                if (remainder >= 0.05M)
                {
                    //Figure out how many nickels
                    nickels = (int)(remainder / 0.05M);
                    remainder = remainder % 0.05M;
                }

                //Check if reaminder is larger than a penny
                if (remainder >= 0.01M)
                {
                    //Figure out how many pennies
                    pennies = (int)(remainder / 0.01M);
                    remainder = remainder % 0.01M;
                }

                //Return the change to the user
                Console.WriteLine();
                Console.WriteLine($"Dispensing Change: {CurrentBalance:C}");
                Console.WriteLine($"Quarters: {quarters}");
                Console.WriteLine($"Dimes: {dimes}");
                Console.WriteLine($"Nickels: {nickels}");
                Console.WriteLine($"Pennies: {pennies}");
                Console.WriteLine();

                //Log the change returned
                Logger.Log($"{DateTime.Now} GIVE CHANGE: {CurrentBalance:C} {0:C}");

                //Update current balance to 0
                CurrentBalance = 0;

                //Go back top the main menu
                DisplayMainMenu();
            }   
        }
    }
}
