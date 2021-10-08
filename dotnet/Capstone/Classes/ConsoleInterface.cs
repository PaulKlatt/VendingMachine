using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class ConsoleInterface
    {
        public VendingMachine vendingMachine = new VendingMachine();
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

        public  void ProcessMainMenuInput(string userInput)
        {
            while (userInput != "1" && userInput != "2" && userInput != "3")
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Enter a numerical menu option displayed above: ");
                userInput = Console.ReadLine();
            }

            if (userInput == "1")
            {
                DisplayInventory();
                DisplayMainMenu();
            }
            else if (userInput == "2")
            {
                DisplayPurchaseMenu();
            }
            else if (userInput == "3")
            {
                //Log that Vending Machine has ended
                Logger.Log($"*** End of Vending Machine Operation: {DateTime.Now} ***");
            }
        }
        public  void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("Current Product Inventory");
            foreach (KeyValuePair<string, VendingMachineItem> inv in vendingMachine.Inventory)
            {
                string quantity = inv.Value.QuantityRemaining.ToString();
                if (inv.Value.QuantityRemaining == 0)
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
            Console.WriteLine($"Current Balance: {vendingMachine.CurrentBalance:C}");
            Console.WriteLine();
            Console.WriteLine("Enter a numerical menu option displayed above: ");
            string userInput = Console.ReadLine();
            ProcessPurchaseMenuInput(userInput);
        }

        public  void ProcessPurchaseMenuInput(string userInput)
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
            while (!isValid || result <= 0)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Please enter a whole dollar (no cents) amount:");
                inputMoney = Console.ReadLine();
                isValid = int.TryParse(inputMoney, out result);
            }
            vendingMachine.CurrentBalance += result;

            //Log that money has been feed
            Logger.Log($"{DateTime.Now} FEED MONEY: {result:C} {vendingMachine.CurrentBalance:C}");

            Console.WriteLine();
            DisplayPurchaseMenu();
        }
    }
}
