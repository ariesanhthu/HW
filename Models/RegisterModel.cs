using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Hãy nhập email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu!")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Hãy nhập lại mật khẩu!")]
        [Compare("ConfirmPassword", ErrorMessage = "Mật khẩu không khớp")]
        [Display(Name = "Xác nhận lại mật khẩu")]
        public string ConfirmPassword { get; set; }
    }
}
