using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-Mail girilmedi")]
        [Display(Name = "E-Mail Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Parola girilmedi")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int SelectedDepId { get; set; }
        
    }
}
