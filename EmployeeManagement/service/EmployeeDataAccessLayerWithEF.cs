using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class EmployeeDataAccessLayerWithEF
    {
        private readonly EmployeeContext _context;

        public EmployeeDataAccessLayerWithEF(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeEF> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public void AddEmployee(EmployeeEF employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(EmployeeEF employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public EmployeeEF GetEmployeeData(int? id)
        {
            return _context.Employees.Find(id);
        }

        public void DeleteEmployee(int? id)
        {
            EmployeeEF employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public void DeleteAllEmployees()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Employees");
            _context.SaveChanges();
        }
    }
}
