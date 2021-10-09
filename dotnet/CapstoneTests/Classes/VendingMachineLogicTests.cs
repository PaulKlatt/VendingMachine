using System;
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

        [TestMethod]
        public void CreateChangeInCoinsTest_41cents_Returns_1quarter_1dime_1nickel_1penny()
        {
            // Arrange
            VendingMachineLogic newVM = new VendingMachineLogic();
            Dictionary<string, int> expected = new Dictionary<string, int>();
            newVM.AddFunds(0.41M);
            expected["Quarters"] = 1;
            expected["Dimes"] = 1;
            expected["Nickels"] = 1;
            expected["Pennies"] = 1;

            // Act
            Dictionary<string, int> actual = newVM.CreateChangeInCoins();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);

        }

        [TestMethod]
        public void CreateChangeInCoinsTest_0cents_Returns_Empty_Dictionary()
        {
            // Arrange
            VendingMachineLogic newVM = new VendingMachineLogic();
            Dictionary<string, int> expected = new Dictionary<string, int>();

            // Act
            Dictionary<string, int> actual = newVM.CreateChangeInCoins();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);

        }

        [TestMethod]
        public void CreateChangeInCoinsTest_Balance_50cents_Returns_2quarters()
        {
            // Arrange
            VendingMachineLogic newVM = new VendingMachineLogic();
            Dictionary<string, int> expected = new Dictionary<string, int>();
            newVM.AddFunds(0.50M);
            expected["Quarters"] = 2;

            // Act
            Dictionary<string, int> actual = newVM.CreateChangeInCoins();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);

        }

        [TestMethod]
        public void AddFundsTest_Balance_0_Funds_1_Returns_1()
        {
            // Arrange
            VendingMachineLogic newVM = new VendingMachineLogic();
            decimal expected = 1M;

            // Act
            decimal actual = newVM.AddFunds(1M);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddFundsTest_Balance_1_Funds_1_Returns_2()
        {
            // Arrange
            VendingMachineLogic newVM = new VendingMachineLogic();
            newVM.AddFunds(1M);
            decimal expected = 2M;

            // Act
            decimal actual = newVM.AddFunds(1M);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DispenseItemTest_A1_Returns_()
        {
            // Arrange
            VendingMachineLogic newVendingMachine = new VendingMachineLogic();
            string filePath = Path.Combine(Environment.CurrentDirectory, "testInputFile.csv");
            Chip expected = new Chip("Potato Crisps", 3.05M, "A1");
            expected.QuantityRemaining--; 
            newVendingMachine.Restock(filePath);

            // Act
            VendingMachineItem actual = newVendingMachine.DispenseProduct("A1");

            // Assert
            Assert.AreEqual(expected.ProductName, actual.ProductName);
            Assert.AreEqual(expected.ProductCost, actual.ProductCost);
            Assert.AreEqual(expected.Slot, actual.Slot);
            Assert.AreEqual(expected.QuantityRemaining, actual.QuantityRemaining);
            Assert.AreEqual(expected.GetPurchaseMesssage(), actual.GetPurchaseMesssage());
        }
    }
}
