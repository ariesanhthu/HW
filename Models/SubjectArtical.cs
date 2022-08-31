using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class SubjectArtical
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public int Total { get; set; }
    }
}
