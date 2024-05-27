using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("employee")] // Attributo EF
    public class EmployeeEF
    {
        [Key] // Attributo EF
        [Column("ID")] // Attributo EF
        public int ID { get; set; }

        [Required] // Attributo EF
        [Column("Name")] // Attributo EF
        public string Name { get; set; }

        [Required] // Attributo EF
        [Column("Gender")] // Attributo EF
        public string Gender { get; set; }

        [Required] // Attributo EF
        [Column("Department")] // Attributo EF
        public string Department { get; set; }

        [Required] // Attributo EF
        [Column("City")] // Attributo EF
        public string City { get; set; }
    }
}
