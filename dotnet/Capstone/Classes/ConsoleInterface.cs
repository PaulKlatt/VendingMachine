using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class ConsoleInterface
    {
        public VendingMachineLogic vendingMachine = new VendingMachineLogic();
        
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
                CalculateReturnChange();

                DisplayMainMenu();
            }
        }
        
        public void FeedMoney()
        {
            //Prompt the user for the funds to add
            Console.WriteLine();
            Console.WriteLine("Please enter a whole dollar amount (no cents) to add to your purchase balance:");
            Console.WriteLine();
            
            //Get the user's input
            string inputMoney = Console.ReadLine();
            
            //Validate the user's input
            bool isValid = int.TryParse(inputMoney, out int result);
            while (!isValid || result <= 0)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Please enter a whole dollar (no cents) amount:");
                inputMoney = Console.ReadLine();
                isValid = int.TryParse(inputMoney, out result);
            }

            //Call back end to add the valid entered funds
            vendingMachine.AddFunds(result);
           
            Console.WriteLine();
            DisplayPurchaseMenu();
        }

        public void SelectProduct()
        {
            //Display the inventory the console
            DisplayInventory();
            
            //Prompt user for the product to purchse
            Console.WriteLine();
            Console.WriteLine("Enter the code corresponding to the product you want:");
            Console.WriteLine();
            
            //Get user input
            string slot = Console.ReadLine();

            //Validate that user can buy the product they selected
            if (!vendingMachine.Inventory.ContainsKey(slot))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid code entered.  Please recheck your desired code and try again.");
                DisplayPurchaseMenu();
            }
            else if (vendingMachine.Inventory[slot].QuantityRemaining == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Product is sold out.  Sorry!  Please select another product and try again.");
                DisplayPurchaseMenu();
            }
            else if (vendingMachine.CurrentBalance < vendingMachine.Inventory[slot].ProductCost)
            {
                Console.WriteLine();
                Console.WriteLine("Insufficient funds.  Please feed more money to make this purchase.");
                DisplayPurchaseMenu();
            }
            else
            {
                //Call the back end to purchase the product in the given slot
                VendingMachineItem itemPurchased = vendingMachine.DispenseProduct(slot);

                //Write to the console what product was purchased
                Console.WriteLine();
                Console.WriteLine("DISPENSING PRODUCT");
                Console.WriteLine($"{itemPurchased.ProductName}: {itemPurchased.ProductCost:C}");
                Console.WriteLine(itemPurchased.PurchaseMessage);
                
                DisplayPurchaseMenu();
            }
        }

        public void CalculateReturnChange()
        {
            decimal balanceBeforeChangeReturn = vendingMachine.CurrentBalance;

            Dictionary<string, int> coinsToReturn = vendingMachine.CreateChangeInCoins();

            string returnCoinsMessage = $"Dispensing Change: {balanceBeforeChangeReturn:C}";
            
            foreach (KeyValuePair<string, int> coins in coinsToReturn)
            {
                returnCoinsMessage += $"\n{coins.Key}: {coins.Value}";
            }

            //Return the change to the user
            Console.WriteLine();
            Console.WriteLine(returnCoinsMessage);
            Console.WriteLine();
        }
    }
}
