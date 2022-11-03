using HW.Models;
using HW.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
namespace HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
             IEmailService emailService,
             ApplicationDbContext context)
        {
            _logger = logger;
            _emailService = emailService;
            _context = context;
        }

        public IActionResult Index()
        {
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmails = new List<string>() { "aries.anhthu@gmail.com" },
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}", "hahahahJonh")
            //    }
            //};
            //await _emailService.SendTestEmail(options);
            //var posts = _context.PostNewFeeds.OrderBy(x => x.Id).ToList();
            //if (posts != null)
            //    return View(posts);
            //else
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
