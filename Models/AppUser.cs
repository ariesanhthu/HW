using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ và tên đệm")]
        public string LastName { get; set; }
        public string AvartarImg { get; set; }
    }
}
