using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace HW.Controllers
{
    public class PostNewFeedController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SubjectArtical Obj { get; set; }
        public PostNewFeedController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? IdTopic)
        {
            ViewBag.PostTopicId = IdTopic;
            if (IdTopic == null) return View(await _context.PostNewFeeds.ToListAsync());

            Obj = new SubjectArtical();
            Obj = await _context.SubjectArticals.FirstOrDefaultAsync(x => x.Id == IdTopic);
            
            if (Obj == null) return View(await _context.PostNewFeeds.ToListAsync());

            var posts = await _context.PostNewFeeds.OrderBy(x => x.OnCreated)
                                            .Where(x => x.SubjectId == Obj.Id).ToListAsync();
            
            if (posts == null) return View(await _context.PostNewFeeds.ToListAsync());
            ViewBag.TopicName = Obj.Name;
            ViewBag.Total = Obj.Total;

            return View(posts);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var Obj = await _context.PostNewFeeds.FirstOrDefaultAsync(x => x.Id == Id);
            if (Obj == null) return View();
            return View(Obj);
        }
    }
}
