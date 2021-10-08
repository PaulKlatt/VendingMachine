using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests.Classes
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
            string actualResult = newCandy.GetPurchaseMesssage();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
