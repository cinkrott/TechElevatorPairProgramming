using Capstone.DAL;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Transactions;


namespace Capstone.Tests
{
    [TestClass]
    public class VenueDAOTest : ParentTest
    {
        [TestMethod]
        public void CanCreateVenueObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(venueDAO);

        }

        [TestMethod]
        public void GetAllVenuesShouldReturnAllVenues()
        {
            // Arrange

            // Act
            List<Venue> venues = venueDAO.GetAllVenues();

            // Assert
            Assert.AreEqual(1, venues.Count);
        }

        [TestMethod]
        public void GetCityReturnsCharlieVille()
        {
            // Arrange

            // Act
            string cityName = venueDAO.GetCity(1);

            // Assert
            Assert.AreEqual("CharlieVille", cityName);
        }

        [TestMethod]
        public void GetStateReturnsOhio()
        {
            // Arrange

            // Act
            string stateName = venueDAO.GetState(1);

            // Assert
            Assert.AreEqual("Ohio", stateName);
        }

        [TestMethod]
        public void GetCategoryReturnsParty()
        {
            // Arrange
            Venue testVenue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            List<string> partyNames = venueDAO.GetCategories(testVenue);

            // Assert
            Assert.AreEqual("Party", partyNames[0]);
        }
    }
}
