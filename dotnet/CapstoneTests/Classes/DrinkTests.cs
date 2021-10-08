using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class DrinkTests
    {
        [TestMethod]
        public void Drink_Constructor_Set_PurchaseMessage()
        {
            //Arrange
            //C1|Cola|1.25|Drink
            string expectedResult = "Glug Glug, Yum!";

            //Act
            Drink newBeverage = new Drink("Cola", 1.25M, "C1");
            string actualResult = newBeverage.GetPurchaseMesssage();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
