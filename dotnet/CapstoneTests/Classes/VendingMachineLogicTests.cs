﻿using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class VendingMachineLogicTests
    {
        [TestMethod]
        public void RestockTest()
        {
            //Arrange
            VendingMachineLogic newVendingMachine = new VendingMachineLogic();
            string filePath = Path.Combine(Environment.CurrentDirectory, "testInputFile.csv");
            Dictionary<string, VendingMachineItem> expectedResult = new Dictionary<string, VendingMachineItem>();

            Chip newChip = new Chip("Potato Crisps", 3.05M, "A1");
            expectedResult["A1"] = newChip;

            Candy newCandy = new Candy("Moonpie", 1.80M, "B1");
            expectedResult["B1"] = newCandy;

            Drink newBeverage = new Drink("Cola", 1.25M, "C1");
            expectedResult["C1"] = newBeverage;

            Gum newGum = new Gum("U-Chews", 0.85M, "D1");
            expectedResult["D1"] = newGum;

            //Act
            newVendingMachine.Restock(filePath);
            Dictionary<string, VendingMachineItem> actualResult = newVendingMachine.Inventory;

            //Assert
            Assert.AreEqual(expectedResult["A1"].QuantityRemaining, actualResult["A1"].QuantityRemaining);
            Assert.AreEqual(expectedResult["A1"].ProductName, actualResult["A1"].ProductName);
            Assert.AreEqual(expectedResult["A1"].ProductCost, actualResult["A1"].ProductCost);
            Assert.AreEqual(expectedResult["A1"].GetPurchaseMesssage(), actualResult["A1"].GetPurchaseMesssage());
            Assert.AreEqual(expectedResult["A1"].Slot, actualResult["A1"].Slot);
        }
    }
}
