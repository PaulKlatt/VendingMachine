using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes.VendingMachineItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests.Classes.VendingMachineItems
{
    [TestClass]
    public class BeverageTests
    {
        [TestMethod]
        public void Beverage_Constructor_Set_PurchaseMessage()
        {
            //Arrange
            //C1|Cola|1.25|Drink
            string expectedResult = "Glug Glug, Yum!";

            //Act
            Beverage newBeverage = new Beverage("Cola", 1.25M, "C1");
            string actualResult = newBeverage.PurchaseMessage;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
