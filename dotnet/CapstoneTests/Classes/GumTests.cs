using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class GumTests
    {
        [TestMethod]
        public void Gum_Constructor_Set_PurchaseMessage()
        {
            //Arrange
            //D4|Triplemint|0.75|Gum
            string expectedResult = "Chew Chew, Yum!";

            //Act
            Gum newGum = new Gum("Triplemint", 0.75M, "D4");
            string actualResult = newGum.GetPurchaseMesssage();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
