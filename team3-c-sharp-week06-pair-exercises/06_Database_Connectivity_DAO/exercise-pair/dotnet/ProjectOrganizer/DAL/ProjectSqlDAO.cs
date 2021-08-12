using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private readonly string connectionString;

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            IList<Project> projects = new List<Project>();
            string sqlGetAllProjects = "SELECT * FROM project";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetAllProjects, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read() == true)
                    {
                        Project project = new Project();

                        project.ProjectId = Convert.ToInt32(reader["project_id"]);
                        project.Name = Convert.ToString(reader["name"]);
                        project.StartDate = Convert.ToDateTime(reader["from_date"]);
                        project.EndDate = Convert.ToDateTime(reader["to_date"]);
                        

                        projects.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Project>();
            }

            return projects;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            bool successful = false;
            string sqlAssignEmployeeToProject = "INSERT INTO project_employee (project_id, employee_id) VALUES (@projectid, @employeeid);";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAssignEmployeeToProject, conn);
                    cmd.Parameters.AddWithValue("projectid", projectId);
                    cmd.Parameters.AddWithValue("employeeid", employeeId);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                    successful = true;
                    }

                }
            }
            catch
            {
                return successful;
            }

            return successful;
        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            bool successful = false;
            string sqlRemoveEmployeeFromProject = "DELETE FROM project_employee WHERE project_id = @projectid AND employee_id = @employeeid;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlRemoveEmployeeFromProject, conn);
                    cmd.Parameters.AddWithValue("@projectid", projectId);
                    cmd.Parameters.AddWithValue("@employeeid", employeeId);

                    int count = cmd.ExecuteNonQuery();
                    if(count > 0)
                    {
                    successful = true;
                    }
                }
            }
            catch
            {
                return successful;
            }

            return successful;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int result = 0;
            string sqlCreateProject = "INSERT INTO project (name, from_date, to_date) VALUES (@name, @fromdate, @todate)";
            string sqlProjectId = "SELECT project_id FROM project WHERE name = @name";
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlCreateProject, conn);
                    cmd.Parameters.AddWithValue("@fromdate", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@todate", newProject.EndDate);

                    int count = cmd.ExecuteNonQuery();
                    if(count > 0)
                    {
                        SqlCommand getId = new SqlCommand(sqlProjectId, conn);
                        getId.Parameters.AddWithValue("@name", newProject.Name);
                        SqlDataReader reader = getId.ExecuteReader();
                        if(reader.Read())
                        {
                            result = Convert.ToInt32(reader["project_id"]);
                        }
                    }
                }

            }
            catch
            {
                return result;
            }

            return result;
        }

    }
}
