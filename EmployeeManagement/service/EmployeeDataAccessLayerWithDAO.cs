using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EmployeeManagement.Models
{
    public class EmployeeDataAccessLayerWithDAO
    {
        string connectionString = "Server=localhost;Database=employee;User Id=employee;Password=employee;";

        // To View all employees details    
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee";
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Department = reader["Department"].ToString(),
                        City = reader["City"].ToString()
                    };

                    lstemployee.Add(employee);
                }

                reader.Close();
                connection.Close();
            }

            return lstemployee;
        }

        // To Add new employee record    
        public void AddEmployee(Employee employee)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Employee (Name, Gender, Department, City) VALUES (@Name, @Gender, @Department, @City)";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@City", employee.City);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // To Update the records of a particular employee  
        public void UpdateEmployee(Employee employee)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET Name = @Name, Gender = @Gender, Department = @Department, City = @City WHERE ID = @EmpId";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@EmpId", employee.ID);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@City", employee.City);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // Get the details of a particular employee  
        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee WHERE ID = @EmpId";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@EmpId", id);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employee.ID = Convert.ToInt32(reader["ID"]);
                    employee.Name = reader["Name"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.Department = reader["Department"].ToString();
                    employee.City = reader["City"].ToString();
                }

                reader.Close();
                connection.Close();
            }

            return employee;
        }

        // To Delete the record on a particular employee  
        public void DeleteEmployee(int? id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Employee WHERE ID = @EmpId";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@EmpId", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
