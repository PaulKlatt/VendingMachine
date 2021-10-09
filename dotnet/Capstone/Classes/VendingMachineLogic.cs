using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachineLogic
    {
        public Dictionary<string, VendingMachineItem> Inventory { get; set; } = new Dictionary<string, VendingMachineItem>();

        public decimal CurrentBalance { get; private set; } = 0;

        public VendingMachineLogic()
        {
            
        }

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
                            Chip newChips = new Chip(productName, productCost, slot);
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
                            Drink newBeverage = new Drink(productName, productCost, slot);
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

        public Dictionary<string, int> CreateChangeInCoins()
        {
            Dictionary<string, int> changeToReturn = new Dictionary<string, int>();

            //Check that there is money to return
            if (CurrentBalance == 0)
            {
                //Log the change returned
                Logger.Log($"{DateTime.Now} GIVE CHANGE: {CurrentBalance:C} {0:C}");

                return changeToReturn;
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

                //Sent the coins to return to the UI
                if (quarters > 0)
                {
                    changeToReturn["Quarters"] = quarters;
                }
                if (dimes > 0)
                {
                    changeToReturn["Dimes"] = dimes;
                }
                if (nickels > 0)
                {
                    changeToReturn["Nickels"] = nickels;
                }
                if (pennies > 0)
                {
                    changeToReturn["Pennies"] = pennies;
                }

                //Log the change returned
                Logger.Log($"{DateTime.Now} GIVE CHANGE: {CurrentBalance:C} {0:C}");

                //Update current balance to 0
                CurrentBalance = 0;

                return changeToReturn;
            }   
        }

        public decimal AddFunds(decimal fundsToAdd)
        {
            //Add the entered funds from the UI to the Current Balance
            CurrentBalance += fundsToAdd;

            //Log that money has been feed
            Logger.Log($"{DateTime.Now} FEED MONEY: {fundsToAdd:C} {CurrentBalance:C}");

            //returns the updated balance
            return CurrentBalance;
        }

        public VendingMachineItem DispenseProduct(string slot)
        {
            //Log the product purchased
            Logger.Log($"{DateTime.Now} {Inventory[slot].ProductName} " +
                $"{Inventory[slot].Slot} {CurrentBalance:C} {(CurrentBalance - Inventory[slot].ProductCost):C}");

            //Decrease the quatity by 1
            Inventory[slot].QuantityRemaining--;
            
            //Subtract the cost of the product from the current balance
            CurrentBalance -= Inventory[slot].ProductCost;

            //return the product purchased
            return Inventory[slot];
        }

    }
}
