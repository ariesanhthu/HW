using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HelloWorld.Models;
using Newtonsoft.Json;

namespace HelloWorld.Models
{
    public class WebUser
    {
        [BindProperty]
        public string ImgUrl { get; set; }

        [BindProperty]
        public string Schoolname { get; set; }

        [BindProperty]
        public string Firstname {get; set;}

        [BindProperty]
        public string Lastname {get; set;}

        [BindProperty]
        public string Classname { get; set; }

        [BindProperty]
        public int NumSolution { get; set; }

        [BindProperty]
        public int NumView { get; set; }

        [BindProperty]
        public List<string> NameLesson { get; set; }

        [Required(ErrorMessage = "Hãy nhập tài khoản")]
        [BindProperty]
        [Display(Name ="Tên đăng nhập")]
        public string Username {get; set;}

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password {get; set;}

        [BindProperty]
        public string Role {get; set;}

        [BindProperty]
        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe {get; set;}

        [BindProperty]
        public bool IsLogin {get; set;}
    }
}
