using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {
        [TestMethod]
        public void Check_that_catering_object_is_created()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act

            //Assert
            Assert.IsNotNull(catering);
        }
        [TestMethod]
        public void AddMoneyWith100Dollars()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act
            decimal result = catering.AddMoney(100);
            //Assert
            Assert.AreEqual(100,result);
        }
        [TestMethod]
        public void AddMoneyAbove5000()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 4995;

            // Act
            decimal result = catering.AddMoney(10);
            //Assert
            Assert.AreEqual(4995, result);
        }
        [TestMethod]
        public void AdjustInventoryItemNotFound()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act
            string result = catering.AdjustInventory("C1", 25);
            //Assert
            Assert.AreEqual("Item does not exist.", result);
        }
        [TestMethod]
        public void AdjustInventoryInsufficientStock()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 200;
            // Act
            string result = catering.AdjustInventory("B1", 60);
            //Assert
            Assert.AreEqual("Insufficient stock.", result);
        }
        [TestMethod]
        public void AdjustInventoryInsufficientFunds()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 20;
            // Act
            string result = catering.AdjustInventory("B1", 50);
            //Assert
            Assert.AreEqual("Insufficient funds.", result);
        }
        [TestMethod]
        public void AdjustInventorySoldOut()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 200;
            // Act
            catering.AdjustInventory("B1", 50);
            string result = catering.AdjustInventory("B1", 1);

            //Assert
            Assert.AreEqual("That item is sold out.", result);
        }
        [TestMethod]
        public void AdjustInventorySuccess()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 200;
            // Act
            string result = catering.AdjustInventory("B1", 25);
            //Assert
            Assert.AreEqual("Purchase successful.", result);
        }

        [TestMethod]
        public void MakeChangeFor999DollarsAnd95Cents()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 999.95M;

            Dictionary<string, int> result = new Dictionary<string, int>();
            result["20"] = 49;
            result["10"] = 1;
            result["5"] = 1;
            result["1"] = 4;
            result[".25"] = 3;
            result[".10"] = 2;
            result[".05"] = 0;


            // Act
            Dictionary<string, int> actual = catering.MakeChange(catering.Balance);

            //Assert
            CollectionAssert.AreEquivalent(actual, result);
        }
        [TestMethod]
        public void MakeChangeFor69()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.Balance = 69;

            Dictionary<string, int> result = new Dictionary<string, int>();
            result["20"] = 3;
            result["10"] = 0;
            result["5"] = 1;
            result["1"] = 4;
            


            // Act
            Dictionary<string, int> actual = catering.MakeChange(catering.Balance);

            //Assert
            CollectionAssert.AreEquivalent(actual, result);
        }


    }
}
