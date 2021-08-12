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
    public class DepartmentSqlDAOTests
    {
        DepartmentSqlDAO departmentDAO;
        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";
        private TransactionScope tran;

        [TestInitialize]
        public void Setup()
        {
            tran = new TransactionScope();

            // Arrange
            departmentDAO = new DepartmentSqlDAO(connectionString);
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void DepartmentSqlDAOConstructor()
        {
            Assert.IsNotNull(departmentDAO);
        }

        [TestMethod]
        public void GetDepartmentsTest()
        {
            IList<Department> actualDepartments = new List<Department>();
            IList<Department> expectedDepartments = new List<Department>();

            actualDepartments = departmentDAO.GetDepartments();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM department" , conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Department department = new Department();
                    department.Id = Convert.ToInt32(reader["department_id"]);
                    department.Name = Convert.ToString(reader["name"]);                    
                    expectedDepartments.Add(department);
                }
            }

            Assert.AreEqual(expectedDepartments.Count, actualDepartments.Count);
        }

        [TestMethod]
        public void CreateDepartmentTests()
        {
            // Arrange
            Department testDepartment = new Department();
            testDepartment.Id = 58;
            testDepartment.Name = "Floob Enumeration Department";

            // Act

            int expectedResult = 0;
            int actualResult = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand getMaxId = new SqlCommand("SELECT COUNT(*) AS 'count' FROM department", conn);

                SqlDataReader reader = getMaxId.ExecuteReader();

                if (reader.Read())
                {
                    expectedResult = Convert.ToInt32(reader["count"]);
                    expectedResult++;
                }
            }

            departmentDAO.CreateDepartment(testDepartment);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand getMaxId = new SqlCommand("SELECT COUNT(*) AS 'count' FROM department", conn);

                SqlDataReader reader = getMaxId.ExecuteReader();

                if (reader.Read())
                {
                    actualResult = Convert.ToInt32(reader["count"]);
                    
                }
            }

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]

        public void UpdateDepartmentTest()
        {
            //Arrange
            Department department = new Department();
            bool result = false;

            department.Id = 1;
            department.Name = "Department of Dundancy";
            string sqlUpdateDepartmentTest = "SELECT name FROM department WHERE department_id = @departmentid;";
            string actualName = "";

            result = departmentDAO.UpdateDepartment(department);

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlUpdateDepartmentTest, conn);
                cmd.Parameters.AddWithValue("@departmentid", department.Id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    actualName = Convert.ToString(reader["name"]);
                }                
            }
            //Assert
            Assert.AreEqual(department.Name, actualName);
        }
    }
}
