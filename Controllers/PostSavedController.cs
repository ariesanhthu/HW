using HW.Services;
using HW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace HW.Controllers
{
    public class PostSavedController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostSavedController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<PostSavedItem> postsaved = HttpContext.Session.GetJson<List<PostSavedItem>>("PostSaved") ?? new List<PostSavedItem>();

            PostSavedViewModel PostSavedVM = new PostSavedViewModel
            {
                PostSavedItems = postsaved,
                TotalAmount = postsaved.Count,
            };

            return View(PostSavedVM);
        }

        [HttpGet]
        public IActionResult SavePost(int id)
        {
            PostNewFeed postNewsFeed = _context.PostNewFeeds.Find(id);

            List<PostSavedItem> postsaved = HttpContext.Session.GetJson<List<PostSavedItem>>("PostSaved") ?? new List<PostSavedItem>();

            PostSavedItem postItem = postsaved.Where(x => x.PostId == id).FirstOrDefault();

            //Add Save
            if(postItem == null)
            {
                postsaved.Add(new PostSavedItem(postNewsFeed));
                HttpContext.Session.SetJson("PostSaved", postsaved);
                return Json(new { status = true, message = "Đã lưu!",Id="icsave-"+id.ToString() });
            }
            else
            {
                //UnSave
                postsaved.Remove(postItem);

                if (postsaved.Count == 0)
                {
                    HttpContext.Session.Remove("PostSaved");
                }
                else
                {
                    HttpContext.Session.SetJson("PostSaved", postsaved);
                }

                return Json(new {status=false, message = "Đã xóa khỏi mục lưu trữ!", Id = "icsave-"+id.ToString()});
            }
           

            //if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")

            //return ViewComponent("SmallCart");
        }

        [HttpGet]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("PostSaved");

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Redirect(Request.Headers["Referer"].ToString());

            return Ok();
        }
    }
}
