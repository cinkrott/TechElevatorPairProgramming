using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SpaceDAO
    {
        // NOTE: No Console.ReadLine or Console.WriteLine in this class

        private string connectionString;

      

        public SpaceDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Space> SpaceList(SqlDataReader reader)
        {
            List<Space> spaces = new List<Space>();

            while (reader.Read())
            {
                Space space;

                int id = Convert.ToInt32(reader["id"]);
                int venueId = Convert.ToInt32(reader["venue_id"]);
                string name = Convert.ToString(reader["name"]);
                bool isAccessible = Convert.ToBoolean(reader["is_accessible"]);
                decimal dailyRate = Convert.ToDecimal(reader["daily_rate"]);
                int maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                int openFrom = 0;
                int openTo = 0;

                if (!DBNull.Value.Equals(reader["open_from"]) && !DBNull.Value.Equals(reader["open_to"]))
                {
                    openFrom = Convert.ToInt32(reader["open_from"]);
                    openTo = Convert.ToInt32(reader["open_to"]);
                }

                space = new Space(id, venueId, name, isAccessible, openFrom, openTo, dailyRate, maxOccupancy);

                spaces.Add(space);
            }

            return spaces;
        }

        public List<Space> GetVenueSpaces(Venue venue)
        {
            List<Space> spaces = new List<Space>();
            string sqlGetVenueSpaces = "SELECT * FROM space JOIN venue ON venue.id = space.venue_id WHERE venue.id = @venueId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetVenueSpaces, conn);
                cmd.Parameters.AddWithValue("@venueId", venue.Id);

                SqlDataReader reader = cmd.ExecuteReader();

                spaces = SpaceList(reader);
            }

            return spaces;
        }

        public List<Space> SearchSpaceAvailability(Venue venue, DateTime startDate, int numberOfDays, int numberOfAttendees)
        {
            List<Space> spaces = new List<Space>();

            DateTime endDate = startDate.AddDays(numberOfDays + 1);

            string sqlSearchSpaceAvailability = "SELECT * FROM space " +
                "WHERE space.id IN " +
                "(SELECT DISTINCT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE venue.id = @venueId " +
                "AND space.max_occupancy >= @numberOfAttendees " +
                "EXCEPT " +
                "SELECT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE (@startDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@endDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@startDate <= reservation.start_date AND reservation.end_date <= @endDate))";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlSearchSpaceAvailability, conn);
                cmd.Parameters.AddWithValue("@venueId", venue.Id);
                cmd.Parameters.AddWithValue("@numberOfAttendees", numberOfAttendees);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                SqlDataReader reader = cmd.ExecuteReader();

                spaces = SpaceList(reader);

                return spaces;
            }
        }

        public List<Space> SearchSpaceAvailabilityWithoutCategory(Venue venue, DateTime startDate, int numberOfDays,
                                                                  int numberOfAttendees, bool isAccessible, decimal maxDailyBudget)
        {
            List<Space> spaces = new List<Space>();

            DateTime endDate = startDate.AddDays(numberOfDays + 1);

            string sqlSearchSpaceAvailabilityWithoutCategory;

            if (isAccessible)
            {
                sqlSearchSpaceAvailabilityWithoutCategory =
                "SELECT * FROM space " +
                "WHERE space.id IN " +
                "(SELECT DISTINCT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE venue.id = @venueId " +
                "AND space.max_occupancy >= @numberOfAttendees " +
                "AND space.daily_rate <= @maxDailyBudget " +
                "AND space.is_accessible = 1 " +
                "EXCEPT " +
                "SELECT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE (@startDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@endDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@startDate <= reservation.start_date AND reservation.end_date <= @endDate))";
            }
            else
            {
                sqlSearchSpaceAvailabilityWithoutCategory =
                "SELECT * FROM space " +
                "WHERE space.id IN " +
                "(SELECT DISTINCT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE venue.id = @venueId " +
                "AND space.max_occupancy >= @numberOfAttendees " +
                "AND space.daily_rate <= @maxDailyBudget " +
                "EXCEPT " +
                "SELECT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE (@startDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@endDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@startDate <= reservation.start_date AND reservation.end_date <= @endDate))";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlSearchSpaceAvailabilityWithoutCategory, conn);
                cmd.Parameters.AddWithValue("@venueId", venue.Id);
                cmd.Parameters.AddWithValue("@numberOfAttendees", numberOfAttendees);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@isAccessible", isAccessible);
                cmd.Parameters.AddWithValue("@maxDailyBudget", maxDailyBudget);

                SqlDataReader reader = cmd.ExecuteReader();

                spaces = SpaceList(reader);

                return spaces;
            }
        }

        public List<Space> SearchSpaceAvailabilityWithCategory(Venue venue, DateTime startDate, int numberOfDays,
                                                          int numberOfAttendees, bool isAccessible, decimal maxDailyBudget,
                                                          int categoryId)
        {
            List<Space> spaces = new List<Space>();

            DateTime endDate = startDate.AddDays(numberOfDays + 1);

            string sqlSearchSpaceAvailabilityWithCategory;

            if (isAccessible)
            {
                sqlSearchSpaceAvailabilityWithCategory =
                "SELECT * FROM space " +
                "WHERE space.id IN " +
                "(SELECT DISTINCT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "JOIN category_venue ON category_venue.venue_id = venue.id " +
                "JOIN category ON category.id = category_venue.category_id " +
                "WHERE venue.id = @venueId " +
                "AND space.max_occupancy >= @numberOfAttendees " +
                "AND space.daily_rate <= @maxDailyBudget " +
                "AND space.is_accessible = 1 " +
                "AND category.id = @categoryId " +
                "EXCEPT " +
                "SELECT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE (@startDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@endDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@startDate <= reservation.start_date AND reservation.end_date <= @endDate))";
            }
            else
            {
                sqlSearchSpaceAvailabilityWithCategory = 
                "SELECT * FROM space " +
                "WHERE space.id IN " +
                "(SELECT DISTINCT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "JOIN category_venue ON category_venue.venue_id = venue.id " +
                "JOIN category ON category.id = category_venue.category_id " +
                "WHERE venue.id = @venueId " +
                "AND space.max_occupancy >= @numberOfAttendees " +
                "AND space.daily_rate <= @maxDailyBudget " +
                "AND category.id = @categoryId " +
                "EXCEPT " +
                "SELECT space.id FROM space " +
                "JOIN venue ON space.venue_id = venue.id " +
                "JOIN reservation ON reservation.space_id = space.id " +
                "WHERE (@startDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@endDate BETWEEN reservation.start_date AND reservation.end_date) " +
                "OR (@startDate <= reservation.start_date AND reservation.end_date <= @endDate))";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlSearchSpaceAvailabilityWithCategory, conn);
                cmd.Parameters.AddWithValue("@venueId", venue.Id);
                cmd.Parameters.AddWithValue("@numberOfAttendees", numberOfAttendees);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@maxDailyBudget", maxDailyBudget);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);

                SqlDataReader reader = cmd.ExecuteReader();

                spaces = SpaceList(reader);

                return spaces;
            }
        }

        public string GetSpaceNameFromId(int spaceId)
        {
            string spaceName = "";

            string sqlGetSpaceFromId = "SELECT name FROM space WHERE id = @spaceId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetSpaceFromId, conn);
                cmd.Parameters.AddWithValue("@spaceId", spaceId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    spaceName = Convert.ToString(reader["name"]);

                }

                return spaceName;
            }
        }

        public decimal GetSpaceDailyRateFromId(int spaceId)
        {
            decimal spaceRate = 0M;

            string sqlGetSpaceFromId = "SELECT daily_rate FROM space WHERE id = @spaceId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetSpaceFromId, conn);
                cmd.Parameters.AddWithValue("@spaceId", spaceId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    spaceRate = Convert.ToDecimal(reader["daily_rate"]);
                }

                return spaceRate;
            }

        }
    }
}
