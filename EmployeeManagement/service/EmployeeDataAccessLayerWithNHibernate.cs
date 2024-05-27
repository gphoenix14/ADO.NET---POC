using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class EmployeeDataAccessLayerWithNHibernate
    {
        public IEnumerable<Employee> GetAllEmployees()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var employees = session.Query<Employee>().ToList();
                return employees;
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(employee);
                    transaction.Commit();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(employee);
                    transaction.Commit();
                }
            }
        }

        public Employee GetEmployeeData(int? id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return employee;
            }
        }

        public void DeleteEmployee(int? id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var employee = session.Get<Employee>(id);
                    if (employee != null)
                    {
                        session.Delete(employee);
                        transaction.Commit();
                    }
                }
            }
        }

        public void DeleteAllEmployees()
{
    using (ISession session = NHibernateHelper.OpenSession())
    {
        using (ITransaction transaction = session.BeginTransaction())
        {
            session.CreateQuery("DELETE FROM Employee").ExecuteUpdate();
            transaction.Commit();
        }
    }
}

    }
}
