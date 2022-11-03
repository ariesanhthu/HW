using System.ComponentModel.DataAnnotations;
namespace HW.Models
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress, Display(Name = "Nhập email reset mật khẩu")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
