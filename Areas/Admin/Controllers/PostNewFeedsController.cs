using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace HW.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("admin")]
    public class PostNewFeedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public PostNewFeed PostObj { get; set; }
        public PostNewFeedsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            webHostEnvironment = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostNewFeeds.Include(x => x.SubjectArtical).ToListAsync());
        }
        public async Task<IActionResult> Detail (int id)
        {
            PostObj = await _context.PostNewFeeds.FirstOrDefaultAsync(x => x.Id == id);
            if (PostObj == null)
            {
                return NotFound();
            }

            return View(PostObj);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null)
            {
                //create
                ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name");
                ViewBag.Id = 0;
                return View(PostObj);
            }

            //update
            ViewBag.Id = 1;
            PostObj = await _context.PostNewFeeds.FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.SubjectId = new SelectList(_context.SubjectArticals.OrderBy(x => x.Name), "Id", "Name", PostObj.SubjectId);
            if (PostObj == null)
            {
                return NotFound();
            }
            return View(PostObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {
            if (ModelState.IsValid)
            {

                string ImgName = "noimg.png";

                //Thêm ảnh vào thư mục root
                if (PostObj.ImgUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/NewsFeed");

                    ImgName = Guid.NewGuid().ToString() + "_" + PostObj.ImgUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, ImgName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);

                    await PostObj.ImgUpload.CopyToAsync(fs);

                    fs.Close();
                }

                PostObj.Image = ImgName;

                if (PostObj.Id == 0)
                {
                    //create
                    var SubObj = _context.SubjectArticals.Find(PostObj.SubjectId);
                    SubObj.Total++;

                    //ngày tạo
                    PostObj.OnCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    PostObj.OnUpdated = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    _context.SubjectArticals.Update(SubObj);
                    _context.PostNewFeeds.Add(PostObj);
                }
                else
                {
                    //update data
                    // set ngày tháng cập nhật

                    PostObj.OnUpdated = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    _context.PostNewFeeds.Update(PostObj);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(PostObj);
        }

        //API : Index() + Delete()
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.PostNewFeeds.Include(x => x.SubjectArtical).ToListAsync() });
        }

        //Hàm xóa
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var PostFromDb = await _context.PostNewFeeds.FirstOrDefaultAsync(u => u.Id == id);
            if (PostFromDb == null)
            {
                return Json(new { success = false, message = "Lỗi khi đang xóa" });
            }

            // xóa ảnh trong thư mục wwwroot
            if (!string.Equals(PostFromDb.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/NewsFeed");
                string oldImagePath = Path.Combine(uploadsDir, PostFromDb.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            //xóa dữ liệu trong Db
            _context.PostNewFeeds.Remove(PostFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thành công!" });
        }
        #endregion

    }
}
