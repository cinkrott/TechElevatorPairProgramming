using Capstone.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ParentTest
    {
        private TransactionScope trans;

        protected string connectionString;
        protected VenueDAO venueDAO;
        protected SpaceDAO spaceDAO;
        protected ReservationDAO reservationDAO;
        protected CategoryDAO categoryDAO;

        public ParentTest()
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            connectionString = configuration.GetConnectionString("Project");
        }

        [TestInitialize]
        public void Setup()
        {
            trans = new TransactionScope();

            string sql = File.ReadAllText("test-script.sql");

            venueDAO = new VenueDAO(connectionString);
            spaceDAO = new SpaceDAO(connectionString);
            reservationDAO = new ReservationDAO(connectionString);
            categoryDAO = new CategoryDAO(connectionString);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();


                //if (reader.Read())
                //{
                //    this.NewCityId = Convert.ToInt32(reader["newCityId"]);
                //}
            }
        }

        [TestCleanup]
        public void Reset()
        {
            trans.Dispose();
        }


        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}

