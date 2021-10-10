using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public abstract class VendingMachineItem 
    {
        public string ProductName { get; }

        public decimal ProductCost { get; }

        public string Slot { get; }

        public VendingMachineItem(string productName, decimal productCost, string slot)
        {
            ProductName = productName;
            ProductCost = productCost;
            Slot = slot;
        }

        public abstract string GetPurchaseMesssage();
    }
}
