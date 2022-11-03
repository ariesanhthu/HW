using HW.Models;
using HW.Repository;
using HW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.EJ2.Charts;

namespace HW.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    public class ClassroomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly IUserService _userService;
        public ClassroomController(ApplicationDbContext context,
             UserManager<AppUser> userManager,
             IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }
        //create
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Room model)
        {
            Room r = new Room();
            if (ModelState.IsValid)
            {
                r = model;
                r.TeacherId = _userService.GetUserId();
                _context.Rooms.Add(r);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(r);
        }


        List<LineData> chartData = new List<LineData>
            {
                new LineData { xValue = new DateTime(2022, 10, 30), yValue = 21, yValue1 = 28 },
                new LineData { xValue = new DateTime(2022, 11, 01), yValue = 24, yValue1 = 44 },
                new LineData { xValue = new DateTime(2022, 11, 2), yValue = 36, yValue1 = 48 },
            };
        
    //GET: detail
    public async Task<IActionResult> Detail(string Id)
        {
            var Obj = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomId == Id);
            ViewBag.dataSource = chartData;
            return View(Obj);
        }
    }
}
