using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class ArticalCreditController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ArticalCredit ArtObj { get; set; }
        public ArticalCreditController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ArtObj = new ArticalCredit();

            if (id == null)
            {
                //create
                return View(ArtObj);
            }

            //update
            ArtObj = _context.ArticalCredits.FirstOrDefault(u => u.Id == id);
            if (ArtObj == null)
            {
                return NotFound();
            }
            return View(ArtObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {

                if (ArtObj.Id == 0)
                {
                    _context.ArticalCredits.Add(ArtObj);
                }
                else
                {
                    _context.ArticalCredits.Update(ArtObj);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ArtObj);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.ArticalCredits.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var PostFromDb = await _context.ArticalCredits.FirstOrDefaultAsync(u => u.Id == id);
            if (PostFromDb == null)
            {
                return Json(new { success = false, message = "Lỗi khi đang xóa" });
            }
            _context.ArticalCredits.Remove(PostFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
