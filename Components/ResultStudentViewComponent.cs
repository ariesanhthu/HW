using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HW.Components
{
    public class ResultStudentViewComponent : ViewComponent
    {
        //private readonly ApplicationDbContext _context;

        //public ResultStudentViewComponent(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public IViewComponentResult Invoke()
        {
            ViewBag.TotalWarning = 1; 
            ViewBag.TotalView = 1;
            ViewBag.TotalAccept = 1;
            ViewBag.TotalErrorr = 1;
            ViewBag.TotalWarning = 1;
            return View();
        }
    }
}
