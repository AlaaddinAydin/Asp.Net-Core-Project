using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Lütfen rol adı giriniz")]
        public string name { get; set; }
    }
}
