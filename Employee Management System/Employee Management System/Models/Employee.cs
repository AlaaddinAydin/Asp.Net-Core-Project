using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string? Image { get; set; }

        public int? Salary { get; set; }

        [ForeignKey("Department")]
        public int? departmentId { get; set; }

        public Department? Department { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        AppUser? AppUser { get; set; }
    }
}
