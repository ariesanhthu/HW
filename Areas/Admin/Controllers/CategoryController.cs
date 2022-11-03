using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Category Obj { get; set; }
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.Include(x => x.SubjectArtical).ToListAsync());
        }
        public IActionResult Upsert(int? id)
        {
            var Obj = new Category();

            if (id == null)
            {
                //create
                ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name");
                return View(Obj);
            }

            //update
            Obj = _context.Categories.FirstOrDefault(u => u.Id == id);
            ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name", Obj.SubjectId);
            if (Obj == null)
            {
                return NotFound();
            }
            return View(Obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {

                if (Obj.Id == 0)
                {
                    _context.Categories.Add(Obj);
                }
                else
                {
                    _context.Categories.Update(Obj);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Obj);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Categories.Include(x => x.SubjectArtical).ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var PostFromDb = await _context.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (PostFromDb == null)
            {
                return Json(new { success = false, message = "Lỗi khi đang xóa" });
            }
            _context.Categories.Remove(PostFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thành công!" });
        }
        #endregion
    }
}
