using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace Employee_Management_System.Models
{
    public class AppUser : IdentityUser
    {
        public int? Pace { get; set; }

        public int? Milaege { get; set; }

        [ForeignKey("Department")]
        public int departmentId { get; set; }

        public Department Department { get; set; }


    }
}
