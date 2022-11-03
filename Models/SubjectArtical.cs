using HW.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW.Models
{
    public class SubjectArtical
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Icon { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile IconUpload { get; set; }
        
        public int Total { get; set; }
    }
}
