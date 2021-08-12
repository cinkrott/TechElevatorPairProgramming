using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.Models;
using ProjectOrganizer.DAL;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class ProjectSqlDAOTests
    {
        ProjectSqlDAO projectDAO;
        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";
        private TransactionScope tran;

        [TestInitialize]
        public void Setup()
        {
            tran = new TransactionScope();

            // Arrange
            projectDAO = new ProjectSqlDAO(connectionString);
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllProjectsTest()
        {
            IList<Project> actualProjects = new List<Project>();
            IList<Project> expectedProjects = new List<Project>();

            actualProjects = projectDAO.GetAllProjects();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM project", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(reader["project_id"]);
                    project.Name = Convert.ToString(reader["name"]);
                    project.StartDate = Convert.ToDateTime(reader["from_date"]);
                    project.EndDate = Convert.ToDateTime(reader["to_date"]);
                    expectedProjects.Add(project);
                }
            }

            Assert.AreEqual(expectedProjects.Count, actualProjects.Count);
        }

        [TestMethod]
        public void AssignEmployeeToProjectTest()
        {
            // Arrange
            int startingRowCount;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM project_employee", conn);
                startingRowCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Act
            projectDAO.AssignEmployeeToProject(1, 1);

            int endingRowCount;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM project_employee", conn);
                endingRowCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Assert
            Assert.AreEqual((startingRowCount + 1), endingRowCount);
        }

        [TestMethod]
        public void RemoveEmployeeFromProjectTest()
        {
            // Arrange
            int startingRowCount;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM project_employee", conn);
                startingRowCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Act
            projectDAO.RemoveEmployeeFromProject(5, 9);

            int endingRowCount;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM project_employee", conn);
                endingRowCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Assert
            Assert.AreEqual((startingRowCount - 1), endingRowCount);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            // Arrange
            Project testProject = new Project();
            testProject.Name = "Squirrel Cigar Party Convention";
            testProject.StartDate = Convert.ToDateTime("2021-06-10");
            testProject.EndDate = Convert.ToDateTime("2021-06-13");

            int startingRowCount;
            int startingMaxId;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM project", conn);
                startingRowCount = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand("SELECT MAX(project_id) FROM project", conn);
                startingMaxId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Act
            projectDAO.CreateProject(testProject);


            int endingRowCount;
            int endingMaxId;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM project", conn);
                endingRowCount = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand("SELECT MAX(project_id) FROM project", conn);
                endingMaxId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Assert
            Assert.AreEqual(startingRowCount + 1, endingRowCount);
            Assert.AreEqual(startingMaxId + 1, endingMaxId);
        }
    }
}
