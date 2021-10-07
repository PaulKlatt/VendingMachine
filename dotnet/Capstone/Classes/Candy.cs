using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Candy : VendingMachineItem
    {
        public Candy(string productName, decimal productCost, string slot)
         : base(productName, productCost, slot, "Munch Munch, Yum!")
        {

        }
    }
}
