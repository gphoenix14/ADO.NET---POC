using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace EmployeeManagement.Models
{
    public class EmployeeDataAccessLayerWithDapper
    {
        string connectionString = "Server=localhost;Database=employee;User Id=employee;Password=employee;";

        public IEnumerable<Employee> GetAllEmployees()
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee";
                return db.Query<Employee>(query);
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Employee (Name, Gender, Department, City) VALUES (@Name, @Gender, @Department, @City)";
                db.Execute(query, new { employee.Name, employee.Gender, employee.Department, employee.City });
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET Name = @Name, Gender = @Gender, Department = @Department, City = @City WHERE ID = @ID";
                db.Execute(query, new { employee.Name, employee.Gender, employee.Department, employee.City, employee.ID });
            }
        }

        public Employee GetEmployeeData(int? id)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee WHERE ID = @ID";
                return db.QuerySingleOrDefault<Employee>(query, new { ID = id });
            }
        }

        public void DeleteEmployee(int? id)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Employee WHERE ID = @ID";
                db.Execute(query, new { ID = id });
            }
        }

        public void DeleteAllEmployees()
{
    using (IDbConnection db = new MySqlConnection(connectionString))
    {
        string query = "DELETE FROM Employee";
        db.Execute(query);
    }
}
    }
}
