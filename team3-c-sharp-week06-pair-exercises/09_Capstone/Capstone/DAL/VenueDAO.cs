using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class VenueDAO
    {
        // NOTE: No Console.ReadLine or Console.WriteLine in this class

        private string connectionString;

        //private Venue venue;

        public VenueDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Venue> GetAllVenues()
        {
            List<Venue> venues = new List<Venue>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM venue ORDER BY name", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    int cityId = Convert.ToInt32(reader["city_id"]);
                    string description = Convert.ToString(reader["description"]);
                    Venue venue = new Venue(id, name, cityId, description);
                    venues.Add(venue);

                }
            }
            return venues;
        }

        public string GetCity(int cityId)
        {
            string result = "";
            string sqlGetCity = "SELECT name FROM city WHERE id = @cityId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetCity, conn);
                cmd.Parameters.AddWithValue("@cityId", cityId);

                result = Convert.ToString(cmd.ExecuteScalar());
            }

            return result;
        }

        public string GetState(int cityId)
        {
            string result = "";
            string sqlGetState = "SELECT state.name FROM state JOIN city ON state.abbreviation = city.state_abbreviation WHERE city.id = @cityId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetState, conn);
                cmd.Parameters.AddWithValue("@cityId", cityId);

                result = Convert.ToString(cmd.ExecuteScalar());
            }

            return result;
        }

        public List<string> GetCategories(Venue venue)
        {
            List<string> categories = new List<string>();

            string sqlGetCategories = "SELECT category.name FROM category JOIN category_venue ON category_venue.category_id = category.id JOIN venue ON category_venue.venue_id = venue.id WHERE venue.id = @venueId ORDER BY category.name";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetCategories, conn);
                cmd.Parameters.AddWithValue("@venueId", venue.Id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string categoryName = Convert.ToString(reader["name"]);
                    categories.Add(categoryName);
                }
            }

            return categories;
        }

        public string GetVenueNameFromId(int venueId)
        {
            string venueName = "";

            string sqlGetSpaceFromId = "SELECT name FROM venue WHERE id = @venueId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetSpaceFromId, conn);
                cmd.Parameters.AddWithValue("@venueId", venueId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    venueName = Convert.ToString(reader["name"]);

                }

                return venueName;
            }
        }
    }
}
