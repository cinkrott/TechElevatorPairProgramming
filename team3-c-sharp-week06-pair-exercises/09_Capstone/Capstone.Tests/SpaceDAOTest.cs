using Capstone.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Transactions;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;
using System;

namespace Capstone.Tests
{
    [TestClass]
    public class SpaceDAOTest : ParentTest

    {
        [TestMethod]
        public void CanCreateSpaceObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(spaceDAO);
        }

        [TestMethod]
        public void ListSpacesShouldReturnAllSpaces()
        {
            List<Space> spaces;
            // Arrange
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM space", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                spaces = spaceDAO.SpaceList(reader);
            }

            // Act

            // Assert
            Assert.AreEqual(1, spaces.Count);
        }

        [TestMethod]
        public void GetVenueSpacesShouldReturnAllSpacesInVenue()
        {
            // Arrange
            Venue venue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            List<Space> spaces = spaceDAO.GetVenueSpaces(venue);

            // Assert
            Assert.AreEqual(1, spaces.Count);
        }

        [TestMethod]
        public void SearchSpaceAvailabilityWithCategoryShouldReturnNothingOn5_5_22()
        {
            // Arrange
            List<Space> availableSpaces;
            Venue testVenue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            availableSpaces = spaceDAO.SearchSpaceAvailabilityWithCategory(testVenue, Convert.ToDateTime("05/05/2022"), 5, 10, true, 1500, 1);

            // Assert
            Assert.AreEqual(0, availableSpaces.Count);
        }

        [TestMethod]
        public void SearchSpaceAvailabilityWithoutCategoryShouldReturnNothingFor5000People()
        {
            // Arrange
            List<Space> availableSpaces;
            Venue testVenue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            availableSpaces = spaceDAO.SearchSpaceAvailabilityWithoutCategory(testVenue, Convert.ToDateTime("06/30/2022"), 5, 5000, false, 1000);

            // Assert
            Assert.AreEqual(0, availableSpaces.Count);
        }

        [TestMethod]
        public void SearchSpaceAvailabilityShouldReturnComputerClubFor6_1_22And50People()
        {
            // Arrange
            List<Space> availableSpaces;
            Venue testVenue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            availableSpaces = spaceDAO.SearchSpaceAvailability(testVenue, Convert.ToDateTime("06/1/2022"), 5, 50);

            // Assert
            Assert.AreEqual(1, availableSpaces.Count);
        }

        [TestMethod]
        public void GetSpaceNameFromIdShouldReturnComputerClubFor1()
        {
            // Arrange
            // Act
            string result = spaceDAO.GetSpaceNameFromId(1);

            // Assert
            Assert.AreEqual("Computer Club", result);
        }

        [TestMethod]
        public void GetSpaceDailyRateFromIdShouldReturn500For1()
        {
            // Arrange
            // Act
            decimal result = spaceDAO.GetSpaceDailyRateFromId(1);

            // Assert
            Assert.AreEqual(500.00M, result);
        }

        [TestMethod]
        public void SearchSpaceAvailabilityWithoutCategoryShouldReturnComputerClubFor6_1_22And50People()
        {
            // Arrange
            List<Space> availableSpaces;
            Venue testVenue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            availableSpaces = spaceDAO.SearchSpaceAvailabilityWithoutCategory(testVenue, Convert.ToDateTime("06/1/2022"), 5, 50, true, 500.00M);

            // Assert
            Assert.AreEqual(1, availableSpaces.Count);
        }

        [TestMethod]
        public void SearchSpaceAvailabilityWithCategoryShouldReturnComputerClubFor6_1_22And50PeopleAndCategoryParty()
        {
            // Arrange
            List<Space> availableSpaces;
            Venue testVenue = new Venue(1, "TechElevator", 1, "A cool place to learn C#");

            // Act
            availableSpaces = spaceDAO.SearchSpaceAvailabilityWithCategory(testVenue, Convert.ToDateTime("06/1/2022"), 5, 50, true, 500.00M, 1);

            // Assert
            Assert.AreEqual(1, availableSpaces.Count);
        }
    }
}
