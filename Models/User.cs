using Microsoft.AspNetCore.Mvc;
using HW.Models;
using System.ComponentModel.DataAnnotations;

namespace HW.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
