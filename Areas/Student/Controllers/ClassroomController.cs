using HW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using HW.Models;
namespace HW.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Student")]
    public class ClassroomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public ClassroomController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public IActionResult Index()
        {
            string userId = _userService.GetUserId();
            var ClassList = _context.Rooms.OrderBy(x => x.Name).ToList();
            return View(ClassList);
        }

        [HttpPost]
        public IActionResult GetClass(string Id)
        {
            if (ModelState.IsValid)
            {

                //StudentClass x = new StudentClass();
                //x.ClassId = Id;
                //x.DateRes = DateTime.Now;
                //x.StudentId = _userService.GetUserId();
                //_context.StudentRoom.Add(x);
                //_context.SaveChanges();

                StudentClassViewModel obj = new StudentClassViewModel();
                obj.RoomName = "12TT";
                List<StudentInfo> stu = new List<StudentInfo>()
                {
                    new StudentInfo
                    {
                        StudentName = "Nguyễn Anh Thư"
                    },
                    new StudentInfo
                    {
                        StudentName = "Nguyễn Việt Đức"
                    }
                };

                obj.Students = stu;
                return View(obj);
            }

            return RedirectToAction("Index");
        }
    }
}
