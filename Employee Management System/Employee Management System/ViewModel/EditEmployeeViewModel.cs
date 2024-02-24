using Employee_Management_System.Models;

namespace Employee_Management_System.ViewModel
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public string Surname { get; set; }

        public string? Image { get; set; }

        public int? Salary { get; set; }

        public int? departmentId { get; set; }

        public Department? Department { get; set; }

        public int SelectedDepId { get; set; }
    }
}
