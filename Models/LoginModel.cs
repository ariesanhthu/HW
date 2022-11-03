using System.ComponentModel.DataAnnotations;
namespace HW.Models
{
    public class LoginModel
    {
        //[Required, EmailAddress]
        [Required]
        public string Email { get; set; }

        //[DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        [Required]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}