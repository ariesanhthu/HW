using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password), Display(Name = "Mật khẩu hiện tại")]
        [Compare("CurrentPassword", ErrorMessage = "Kiểm tra lại mật khẩu")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận lại mật khẩu không khớp")]
        public string ConfirmNewPassword { get; set; }
    }
}
