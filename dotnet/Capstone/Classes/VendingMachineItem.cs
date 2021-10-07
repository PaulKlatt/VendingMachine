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

        public int QuantityRemaining { get; set; } = 5;

        public string PurchaseMessage { get; }

        public VendingMachineItem(string productName, decimal productCost, string slot, string purchaseMessage)
        {
            ProductName = productName;
            ProductCost = productCost;
            Slot = slot;
            PurchaseMessage = purchaseMessage;
        }
    }
}
