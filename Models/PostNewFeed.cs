﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HW.Services;
namespace HW.Models
{
    public class PostNewFeed
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Thời gian")]
        public string Time { get; set; }
        public string Image { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual SubjectArtical SubjectArtical { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImgUpload { get; set; }
    }
}
