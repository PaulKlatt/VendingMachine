using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Capstone.Classes;


namespace CapstoneTests.Classes
{   
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void LogTest()
        {
            //Arrange
            string expected = "logTest";
            string filePath = Logger.LogFilePath;
            string actual = "";


            //Act
            Logger.Log(expected);
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    actual = sr.ReadLine();
                }
                    
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                File.Delete(filePath);
            }

            //Assert

            Assert.AreEqual(expected, actual);

        }


    }
}
