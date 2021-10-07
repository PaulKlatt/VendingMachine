using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void RestockTest()
        {
            //Arrange
            VendingMachine newVendingMachine = new VendingMachine();
            string filePath = Path.Combine(Environment.CurrentDirectory, "testInputFile.csv");
            Dictionary<string, VendingMachineItem> expectedResult = new Dictionary<string, VendingMachineItem>();

            Chips newChip = new Chips("Potato Crisps", 3.05M, "A1");
            expectedResult["A1"] = newChip;

            Candy newCandy = new Candy("Moonpie", 1.80M, "B1");
            expectedResult["B1"] = newCandy;

            Beverage newBeverage = new Beverage("Cola", 1.25M, "C1");
            expectedResult["C1"] = newBeverage;

            Gum newGum = new Gum("U-Chews", 0.85M, "D1");
            expectedResult["D1"] = newGum;

            //Act
            newVendingMachine.Restock(filePath);
            Dictionary<string, VendingMachineItem> actualResult = newVendingMachine.Inventory;

            //Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
