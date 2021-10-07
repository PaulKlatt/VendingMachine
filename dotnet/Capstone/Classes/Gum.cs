using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Gum : VendingMachineItem
    {
        public Gum(string productName, decimal productCost, string slot)
            : base(productName, productCost, slot, "Chew Chew, Yum!")
        {

        }
    }
}
