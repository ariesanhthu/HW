using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SubjectArticalController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public SubjectArtical SubObj { get; set; }
        public SubjectArticalController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            SubObj = new SubjectArtical();

            if (id == null)
            {
                //create
                return View(SubObj);
            }

            //update
            SubObj = _context.SubjectArticals.FirstOrDefault(u => u.Id == id);
            if (SubObj == null)
            {
                return NotFound();
            }
            return View(SubObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {

                if (SubObj.Id == 0)
                {
                    _context.SubjectArticals.Add(SubObj);
                }
                else
                {
                    _context.SubjectArticals.Update(SubObj);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(SubObj);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.SubjectArticals.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var PostFromDb = await _context.SubjectArticals.FirstOrDefaultAsync(u => u.Id == id);
            if (PostFromDb == null)
            {
                return Json(new { success = false, message = "Lỗi khi đang xóa" });
            }
            _context.SubjectArticals.Remove(PostFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
