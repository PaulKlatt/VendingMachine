using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Drink : VendingMachineItem
    {
        public Drink(string productName, decimal productCost, string slot) 
            : base(productName, productCost, slot)
        {
            
        }

        public override string GetPurchaseMesssage()
        {
            return "Glug Glug, Yum!";
        }
    }
}
