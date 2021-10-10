using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using System.IO;
using System.Collections;

namespace Capstone.Classes
{
    public class VendingMachineLogic
    {
        public Dictionary<string, Queue<VendingMachineItem>> Inventory { get; set; } = new Dictionary<string, Queue<VendingMachineItem>>();

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
                            Chip newChip1 = new Chip(productName, productCost, slot);
                            Chip newChip2 = new Chip(productName, productCost, slot);
                            Chip newChip3 = new Chip(productName, productCost, slot);
                            Chip newChip4 = new Chip(productName, productCost, slot);
                            Chip newChip5 = new Chip(productName, productCost, slot);
                            Queue<VendingMachineItem> slotQueue = new Queue<VendingMachineItem>();
                            slotQueue.Enqueue(newChip1);
                            slotQueue.Enqueue(newChip2);
                            slotQueue.Enqueue(newChip3);
                            slotQueue.Enqueue(newChip4);
                            slotQueue.Enqueue(newChip5);
                            Inventory[slot] = slotQueue;
                        }
                        else if (productType == "Candy")
                        {
                            Candy newCandy1 = new Candy(productName, productCost, slot);
                            Candy newCandy2 = new Candy(productName, productCost, slot);
                            Candy newCandy3 = new Candy(productName, productCost, slot);
                            Candy newCandy4 = new Candy(productName, productCost, slot);
                            Candy newCandy5 = new Candy(productName, productCost, slot);
                            Queue<VendingMachineItem> slotQueue = new Queue<VendingMachineItem>();
                            slotQueue.Enqueue(newCandy1);
                            slotQueue.Enqueue(newCandy2);
                            slotQueue.Enqueue(newCandy3);
                            slotQueue.Enqueue(newCandy4);
                            slotQueue.Enqueue(newCandy5);
                            Inventory[slot] = slotQueue;
                        }
                        else if (productType == "Gum")
                        {
                            Gum newGum1 = new Gum(productName, productCost, slot);
                            Gum newGum2 = new Gum(productName, productCost, slot);
                            Gum newGum3 = new Gum(productName, productCost, slot);
                            Gum newGum4 = new Gum(productName, productCost, slot);
                            Gum newGum5 = new Gum(productName, productCost, slot);
                            Queue<VendingMachineItem> slotQueue = new Queue<VendingMachineItem>();
                            slotQueue.Enqueue(newGum1);
                            slotQueue.Enqueue(newGum2);
                            slotQueue.Enqueue(newGum3);
                            slotQueue.Enqueue(newGum4);
                            slotQueue.Enqueue(newGum5);
                            Inventory[slot] = slotQueue;
                        }
                        else if (productType == "Drink")
                        {
                            Drink newDrink1 = new Drink(productName, productCost, slot);
                            Drink newDrink2 = new Drink(productName, productCost, slot);
                            Drink newDrink3 = new Drink(productName, productCost, slot);
                            Drink newDrink4 = new Drink(productName, productCost, slot);
                            Drink newDrink5 = new Drink(productName, productCost, slot);
                            Queue<VendingMachineItem> slotQueue = new Queue<VendingMachineItem>();
                            slotQueue.Enqueue(newDrink1);
                            slotQueue.Enqueue(newDrink2);
                            slotQueue.Enqueue(newDrink3);
                            slotQueue.Enqueue(newDrink4);
                            slotQueue.Enqueue(newDrink5);
                            Inventory[slot] = slotQueue;
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
            Logger.Log($"{DateTime.Now} {Inventory[slot].Peek().ProductName} " +
                $"{slot} {CurrentBalance:C} {(CurrentBalance - Inventory[slot].Peek().ProductCost):C}");


            //Subtract the cost of the product from the current balance
            CurrentBalance -= Inventory[slot].Peek().ProductCost;

            //return the product purchased. Decrease the quatity by 1
            return Inventory[slot].Dequeue();
        }

    }
}
