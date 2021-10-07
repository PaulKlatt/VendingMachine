using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes.VendingMachineItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests.Classes.VendingMachineItems
{
    [TestClass]
    public class CandyTests
    {
        [TestMethod]
        public void Candy_Constructor_Set_PurchaseMessage()
        {
            //Arrange
            //B2|Cowtales|1.50|Candy
            string expectedResult = "Munch Munch, Yum!";

            //Act
            Candy newCandy = new Candy("Cowtales", 1.50M, "B2");
            string actualResult = newCandy.PurchaseMessage;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
