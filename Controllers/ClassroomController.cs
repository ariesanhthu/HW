using HW.Repository;
using Microsoft.AspNetCore.Mvc;
using HW.Models;
using System.Collections.Generic;
using Syncfusion.EJ2.Charts;
namespace HW.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IAccountRepository _accRepo;
        public ClassroomController(IAccountRepository accRepo)
        {
            _accRepo = accRepo;
        }
        public class PieData
        {
            public string xValue;
            public double yValue;
            public string text;
        }
        public IActionResult Index()
        {
            PieViewModel dataPie = new PieViewModel();
            List<PieData> chartData = new List<PieData>
            {
                new PieData { xValue =  "Accepted", yValue = 10, text = "33%" },
                new PieData { xValue =  "Wrong Answer", yValue = 10, text = "33%" },
                new PieData { xValue =  "Mistake", yValue = 10, text = "33%" },
            };
            ViewBag.dataSource = chartData;
            return View();
        }
    }
}
