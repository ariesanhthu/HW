using HW.Services;
using Microsoft.AspNetCore.Mvc;
using HW.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HW.Components
{
    public class GetSuggestViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public GetSuggestViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.LibraryArticalClasses.OrderBy(x => x.Id).Where(x => x.SubjectId == 1).ToListAsync());
        }
    }
}
