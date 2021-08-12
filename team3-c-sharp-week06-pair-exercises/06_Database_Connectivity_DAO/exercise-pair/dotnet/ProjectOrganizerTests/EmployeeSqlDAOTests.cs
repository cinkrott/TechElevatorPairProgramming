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
    public class EmployeeSqlDAOTests
    {
        EmployeeSqlDAO employeeDAO;
        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";
        private TransactionScope tran;

        [TestInitialize]
        public void Setup()
        {
            tran = new TransactionScope();

            // Arrange
            employeeDAO = new EmployeeSqlDAO(connectionString);
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void EmployeeSqlDAOConstructor()
        {
            Assert.IsNotNull(employeeDAO);
        }

        [TestMethod]

        public void GetAllEmployeesTest()
        {
            IList<Employee> actualEmployees = new List<Employee>();
            IList<Employee> expectedEmployees = new List<Employee>();

            actualEmployees = employeeDAO.GetAllEmployees();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee Employee = new Employee();
                    Employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                    Employee.FirstName = Convert.ToString(reader["first_name"]);
                    Employee.LastName = Convert.ToString(reader["last_name"]);
                    Employee.JobTitle = Convert.ToString(reader["job_title"]);
                    Employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                    Employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                    Employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                    expectedEmployees.Add(Employee);
                }
            }

            Assert.AreEqual(expectedEmployees.Count, actualEmployees.Count);
        }

        [TestMethod]

        public void SearchTest()
        {
            IList<Employee> actualEmployees = new List<Employee>();            

            actualEmployees = employeeDAO.Search("e", "i");
                                  

            Assert.AreEqual(actualEmployees[0].FirstName, "Meg"); 
            Assert.AreEqual(actualEmployees[0].LastName, "Buskirk");
            Assert.AreEqual(actualEmployees[0].EmployeeId, 9);
        }

        [TestMethod]
        public void GetEmployeeWithoutProjecTest()
        {
            IList<Employee> employees = new List<Employee>();

            employees = employeeDAO.GetEmployeesWithoutProjects();

            Assert.AreEqual(employees[0].FirstName, "Delora");
            Assert.AreEqual(employees[0].LastName, "Coty");
            Assert.AreEqual(employees[0].EmployeeId, 4);
        }
    }
}
