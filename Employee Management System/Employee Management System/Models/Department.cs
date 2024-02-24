using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string DepartmentName { get; set; }
    }
}
