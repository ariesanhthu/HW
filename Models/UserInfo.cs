using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class UserInfo
    {
        public string Email { get; set; }
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ và tên đệm")]
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvartarImg { get; set; }
        public UserInfo() { }   

        public UserInfo(AppUser appUser)
        {
            Email = appUser.Email;
            Password = appUser.PasswordHash;
            UserName = appUser.UserName;
            FirstName = appUser.FirstName;
            LastName = appUser.LastName;
            AvartarImg = appUser.AvartarImg;
        }
    }
}
