using HW.Models;
using Microsoft.EntityFrameworkCore;
namespace HW.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<SubjectArtical> SubjectArticals { get; set; }
        public DbSet<PostNewFeed> PostNewFeeds { get; set; }
        public DbSet<ArticalCredit> ArticalCredits { get; set; }
    }
}
