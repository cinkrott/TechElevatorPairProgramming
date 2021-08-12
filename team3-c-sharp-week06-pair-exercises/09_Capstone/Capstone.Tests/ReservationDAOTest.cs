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
    public class ReservationDAOTest : ParentTest
    {
        [TestMethod]
        public void CanCreateReservationObject()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(reservationDAO);
        }

        [TestMethod]
        public void ReserveSpaceReservesSpace()
        {
            // Arrange
            List<Reservation> reservations = new List<Reservation>();

            // Act
            Reservation result = reservationDAO.ReserveSpace(1, 40, Convert.ToDateTime("8/1/2022"),
                                                             Convert.ToDateTime("8/5/2022"), "Charlie and Brian");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM reservation", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    int id = Convert.ToInt32(reader["reservation_id"]);
                    int spaceId = Convert.ToInt32(reader["space_id"]);
                    int numberOfAttendees = Convert.ToInt32(reader["number_of_attendees"]);
                    DateTime startDate = Convert.ToDateTime(reader["start_date"]);
                    DateTime endDate = Convert.ToDateTime(reader["end_date"]);
                    string reservedFor = Convert.ToString(reader["reserved_for"]);
                    Reservation reservation = new Reservation(id, spaceId, numberOfAttendees, startDate, endDate, reservedFor);

                    reservations.Add(reservation);
                }
            }

            // Assert
            Assert.AreEqual(2, reservations.Count);
        }
    }
}
