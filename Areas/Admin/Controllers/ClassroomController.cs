using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class ClassroomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public EditRoomVM EditObj { get; set; }
        public ClassroomController(ApplicationDbContext context,
            IUserService userService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
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
            if(ModelState.IsValid)
            {
                r = model;
                r.TeacherId = _userService.GetUserId();
                _context.Rooms.Add(r);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(r);
        }

        //Update
        public async Task<IActionResult> Edit(string id)
        {
            var Obj = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomId == id);
            if (Obj == null) return NotFound();

            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var objStu = _context.StudentRooms.FirstOrDefault(x => x.UserId == user.Id);
                if (objStu != null)
                {
                    members.Add(user);
                }
                else
                    nonMembers.Add(user);
            }

            return View(new EditRoomVM
            {
                RoomName = Obj.Name,
                Members = members,
                NonMembers = nonMembers,
                Room = Obj,
                TeacherId = Obj.TeacherId,
                RoomId = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoomVM model)
        {
            var r = new Room
            {
                RoomId = model.RoomId,
                Name = model.RoomName,
                TeacherId = model.TeacherId,
                Description = model.Description,
            };
            _context.Rooms.Update(r);
            
            _context.SaveChanges();
            
            var RoomObj = _context.Rooms.FirstOrDefault(x => x.RoomId == model.RoomId);
            //Duyệt qua các lựa chọn

            foreach (string userId in model.AddIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);

                StudentInfo stu = new StudentInfo {
                    UserId = userId,
                    StudentName = user.LastName + user.FirstName
                };
                _context.StudentInfos.Add(stu);
                
                _context.SaveChanges();
            }


            foreach (string userId in model.AddIds ?? new string[] { })
            {
                var StuObj = _context.StudentInfos.FirstOrDefault(x => x.UserId == userId);
                StudentRoom db = new StudentRoom
                {
                    StudentId = StuObj.StudentId,
                    RoomId = RoomObj.RoomId,
                    UserId = userId,
                };
                _context.StudentRooms.Update(db);
                _context.SaveChanges();
            }


            foreach (string userId in model.DeleteIds ?? new string[] { })
            {
                var studentObj = _context.StudentInfos.FirstOrDefault(x => x.UserId == userId);
                var db = _context.StudentRooms.FirstOrDefault(x => x.RoomId == model.RoomId && x.StudentId == studentObj.StudentId);
                
                _context.StudentRooms.Remove(db);

                _context.SaveChanges();
            }

            return View("Index");
        }

        //API : Index() + Delete()
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Rooms.ToListAsync() });
        }

        //Hàm xóa
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var ObjDb = await _context.Rooms.FirstOrDefaultAsync(u => u.RoomId == id);
            if (ObjDb == null)
            {
                return Json(new { success = false, message = "Lỗi khi đang xóa" });
            }
            //xóa dữ liệu trong Db
            _context.Rooms.Remove(ObjDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
