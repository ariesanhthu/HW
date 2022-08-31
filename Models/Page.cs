using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}
