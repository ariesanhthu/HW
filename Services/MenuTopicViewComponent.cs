using HW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.Services
{
    public class MenuTopicViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuTopicViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topics = await GetTopicsAsync();
            return View(topics);
        }

        private Task<List<SubjectArtical>> GetTopicsAsync()
        {
            return _context.SubjectArticals.OrderBy(x => x.Id).ToListAsync();
        }
    }
}
