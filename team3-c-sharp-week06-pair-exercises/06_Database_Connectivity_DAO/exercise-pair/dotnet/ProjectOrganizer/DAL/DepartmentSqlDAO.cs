using ProjectOrganizer.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private readonly string connectionString;

        private string sqlGetDepartments = "SELECT * FROM department;";

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;

        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            IList<Department> departments = new List<Department>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetDepartments, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read() == true)
                    {
                        Department department = new Department();

                        department.Id = Convert.ToInt32(reader["department_id"]);
                        department.Name = Convert.ToString(reader["name"]);

                        departments.Add(department);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Department>();
            }

            return departments;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int deptId = 0;
            string sqlCreateDepartment = "INSERT INTO department (name) VALUES (@name);";
            string sqlDeptId = "SELECT department_id FROM department WHERE name = @name;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlCreateDepartment, conn);
                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);
                    

                    int count = cmd.ExecuteNonQuery();
                    if(count > 0)
                    {
                        SqlCommand getId = new SqlCommand(sqlDeptId, conn);
                        getId.Parameters.AddWithValue("@name", newDepartment.Name);
                        SqlDataReader reader = getId.ExecuteReader();
                        if (reader.Read())
                        {
                        deptId = Convert.ToInt32(reader["department_id"]);
                        
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return deptId;
            }

            return deptId;
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            bool update = false;
            string sqlUpdatedDepartment = "UPDATE department SET name = @name WHERE department_id = @department_id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlUpdatedDepartment, conn);
                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);
                    cmd.Parameters.AddWithValue("@department_id", updatedDepartment.Id);
                    int count = cmd.ExecuteNonQuery();
                    if(count > 0)
                    {
                     update = true;
                    }
                }
            }
            catch(Exception ex)
            {
                
                return update;
            }




            return update;
        }

    }
}
