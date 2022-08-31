using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class ArticalCredit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên trang")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Đường dẫn")]
        public string Url { get; set; }
    }
}
