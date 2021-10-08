using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chip : VendingMachineItem
    {
        public Chip(string productName, decimal productCost, string slot)
           : base(productName, productCost, slot)
        {

        }

        public override string GetPurchaseMesssage()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
