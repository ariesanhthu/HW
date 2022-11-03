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
    public class SubjectArticalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public SubjectArtical SubObj { get; set; }
        public SubjectArticalController(ApplicationDbContext context,
                                        IWebHostEnvironment env)
        {
            _context = context;
            webHostEnvironment = env;
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
        public async Task<IActionResult> Upsert()
        {
            if (ModelState.IsValid)
            {
                string ImgName = "noimg.png";

                //Thêm ảnh vào thư mục root
                if (SubObj.IconUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "ic/Topic");

                    ImgName = Guid.NewGuid().ToString() + "_" + SubObj.IconUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, ImgName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);

                    await SubObj.IconUpload.CopyToAsync(fs);

                    fs.Close();
                }

                SubObj.Icon = ImgName;
                if (SubObj.Id == 0)
                {
                    //Create
                    _context.SubjectArticals.Add(SubObj);
                }
                else
                {
                    //Update
                    _context.SubjectArticals.Update(SubObj);
                }
                await _context.SaveChangesAsync();
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
