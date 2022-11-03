using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HW.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.CreditId = new SelectList(_context.ArticalCredits.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.IsSearch = false;
            return View();
        }
        [HttpPost]
        public IActionResult Index(LibraryViewModel Obj)
        {
            ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.CreditId = new SelectList(_context.ArticalCredits.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.IsSearch = true;

            var ObjArts = from m in _context.LibraryArticalClasses
                          select m;

            if (!string.IsNullOrEmpty(Obj.Searching))
            {
                ObjArts = ObjArts.Where(x => x.Name.Contains(Obj.Searching));
            }
            if(Obj.SubjectId != 0)
            {
                ObjArts = ObjArts.Where(x => x.SubjectId == Obj.SubjectId);
            }
            //credit
            
            //category
            var ResultObj = new LibraryViewModel();
            ResultObj.Articals = ObjArts.ToList();
            return View(ResultObj);
        }
    }
}
