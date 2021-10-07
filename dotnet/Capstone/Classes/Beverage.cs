using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Beverage : VendingMachineItem
    {
        public Beverage(string productName, decimal productCost, string slot) 
            : base(productName, productCost, slot, "Glug Glug, Yum!")
        {
            
        }
    }
}
