using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên chuyên đề")]
        public string Name { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
    public virtual SubjectArtical SubjectArtical { get; set; }
    }
}
