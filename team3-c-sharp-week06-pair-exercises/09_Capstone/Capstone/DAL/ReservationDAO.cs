using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationDAO
    {
        // NOTE: No Console.ReadLine or Console.WriteLine in this class

        private string connectionString;

        //private Venue venue;

        public ReservationDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Reservation ReserveSpace(int spaceId, int numberOfAttendees, DateTime startDate, DateTime endDate, string reservationName)
        {

            Reservation reservation;

            string sqlReserveSpace = "INSERT INTO reservation (space_id, number_of_attendees, start_date, end_date, reserved_for) " +
                "VALUES (@spaceId, @numberOfAttendees, @startDate, @endDate, @reservationName)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlReserveSpace, conn);
                cmd.Parameters.AddWithValue("@spaceId", spaceId);
                cmd.Parameters.AddWithValue("@numberOfAttendees", numberOfAttendees);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@reservationName", reservationName);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT @@IDENTITY FROM reservation", conn);
                int reservationId = Convert.ToInt32(cmd.ExecuteScalar());
                reservation = new Reservation(reservationId, spaceId, numberOfAttendees, startDate, endDate, reservationName);

                return reservation;

            }
        }
    }
}
