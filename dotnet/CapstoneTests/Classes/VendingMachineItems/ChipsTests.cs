using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes.VendingMachineItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CapstoneTests.Classes.VendingMachineItems
{
    [TestClass]
    public class ChipsTests
    {
        [TestMethod]
        public void Chips_Constructor_Set_PurchaseMessage()
        {
            //Arrange
            //A1|Potato Crisps|3.05|Chips
            string expectedResult = "Crunch Crunch, Yum!";

            //Act
            Chips newChips = new Chips("Potato Crips", 3.05M, "A1");
            string actualResult = newChips.PurchaseMessage;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
