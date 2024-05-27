using NHibernate.Mapping.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    [Class(Table = "Employee")]
    public class Employee
    {
        [Id(Name = "ID", Column = "ID")]
        [Generator(1, Class = "native")]
        public virtual int ID { get; set; }

        [Property(Column = "Name", NotNull = true)]
        public virtual string Name { get; set; }

        [Property(Column = "Gender", NotNull = true)]
        public virtual string Gender { get; set; }

        [Property(Column = "Department", NotNull = true)]
        public virtual string Department { get; set; }

        [Property(Column = "City", NotNull = true)]
        public virtual string City { get; set; }
    }
}
