using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CapstoneTests.Classes
{
    [TestClass]
    public class ChipTests
    {
        [TestMethod]
        public void Chip_Constructor_Set_PurchaseMessage()
        {
            //Arrange
            //A1|Potato Crisps|3.05|Chips
            string expectedResult = "Crunch Crunch, Yum!";

            //Act
            Chip newChips = new Chip("Potato Crips", 3.05M, "A1");
            string actualResult = newChips.GetPurchaseMesssage();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
