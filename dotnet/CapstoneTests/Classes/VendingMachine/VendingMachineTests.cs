using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes.VendingMachineItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CapstoneTests.Classes.VendingMachine
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void RestockTest()
        {
            //Arrange
            string filePath = Path.Combine(Environment.CurrentDirectory, "vendingmachine.csv");
            Dictionary<string, VendingMachineItem> expectedResult = new Dictionary<string, VendingMachineItem>();


            //Act

            //Assert
        }
    }
}
