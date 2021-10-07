using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chips : VendingMachineItem
    {
        public Chips(string productName, decimal productCost, string slot)
           : base(productName, productCost, slot, "Crunch Crunch, Yum!")
        {

        }
    }
}
