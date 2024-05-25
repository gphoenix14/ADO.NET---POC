using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

//USING ADO.NET

namespace EmployeeManagement.Models
{
    public class EmployeeDataAccessLayerWithDAO
    {
        string connectionString = "Server=localhost;Database=employee;User Id=employee;Password=employee;";

        // To View all employees details    
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Employee", con); // Query SQL diretta

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();

                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        // To Add new employee record    
        public void AddEmployee(Employee employee)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employee (Name, Gender, Department, City) VALUES (@Name, @Gender, @Department, @City)";
                    MySqlCommand cmd = new MySqlCommand(query, con); // Query SQL diretta

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@City", employee.City);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine("Errore SQL: " + sqlEx.Message);
                Console.WriteLine("Stato SQL: " + sqlEx.ErrorCode);
                Console.WriteLine("Numero Errore: " + sqlEx.Number);
                Console.WriteLine("Stack Trace: " + sqlEx.StackTrace); // Aggiungi lo stack trace
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore Generale: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace); // Aggiungi lo stack trace
                throw;
            }
        }

        // To Update the records of a particular employee  
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE Employee SET Name = @Name, Gender = @Gender, Department = @Department, City = @City WHERE ID = @EmpId";
                    MySqlCommand cmd = new MySqlCommand(query, con); // Query SQL diretta

                    cmd.Parameters.AddWithValue("@EmpId", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@City", employee.City);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine("Errore SQL: " + sqlEx.Message);
                Console.WriteLine("Stato SQL: " + sqlEx.ErrorCode);
                Console.WriteLine("Numero Errore: " + sqlEx.Number);
                Console.WriteLine("Stack Trace: " + sqlEx.StackTrace); // Aggiungi lo stack trace
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore Generale: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace); // Aggiungi lo stack trace
                throw;
            }
        }

        // Get the details of a particular employee  
        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employee WHERE ID = @EmpId";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con); // Query SQL diretta

                cmd.Parameters.AddWithValue("@EmpId", id);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();
                }
            }
            return employee;
        }

        // To Delete the record on a particular employee  
        public void DeleteEmployee(int? id)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM Employee WHERE ID = @EmpId";
                    MySqlCommand cmd = new MySqlCommand(query, con); // Query SQL diretta

                    cmd.Parameters.AddWithValue("@EmpId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (MySqlException sqlEx)
            {
                Console.WriteLine("Errore SQL: " + sqlEx.Message);
                Console.WriteLine("Stato SQL: " + sqlEx.ErrorCode);
                Console.WriteLine("Numero Errore: " + sqlEx.Number);
                Console.WriteLine("Stack Trace: " + sqlEx.StackTrace); // Aggiungi lo stack trace
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore Generale: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace); // Aggiungi lo stack trace
                throw;
            }
        }
    }

}
