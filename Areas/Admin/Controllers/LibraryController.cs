
using HW.Services;
using HW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public LibraryArticalClass Obj { get; set; }
        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _context.LibraryArticalClasses.Include(x => x.SubjectArtical).ToListAsync());
        }

        public IActionResult Upsert(int? id)
        {
            Obj = new LibraryArticalClass();

            if (id == null)
            {
                //create
                ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name");
                ViewBag.TagCreadits = _context.ArticalCredits.OrderBy(x => x.Name).ToList();
                return View(Obj);
            }

            //update
            Obj = _context.LibraryArticalClasses.FirstOrDefault(u => u.Id == id);
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
                    _context.LibraryArticalClasses.Add(Obj);
                }
                else
                {
                    _context.LibraryArticalClasses.Update(Obj);
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
            return Json(new { data = await _context.LibraryArticalClasses.Include(x => x.SubjectArtical).ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var PostFromDb = await _context.LibraryArticalClasses.FirstOrDefaultAsync(u => u.Id == id);
            if (PostFromDb == null)
            {
                return Json(new { success = false, message = "Lỗi khi đang xóa" });
            }
            _context.LibraryArticalClasses.Remove(PostFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thành công!" });
        }
        #endregion
    }
}
