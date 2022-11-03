using HW.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HW.Services
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().Property(p => p.RoomId).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentInfo>().Property(p => p.StudentId).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentRoom>()
                .HasKey(x => new { x.StudentId, x.RoomId });
            //modelBuilder.Entity<TeacherClass>()
            //    .HasKey(nameof(TeacherClass.TeacherId), nameof(TeacherClass.ClassId));
        }

        //Newsfeed
        public DbSet<SubjectArtical> SubjectArticals { get; set; }
        public DbSet<PostNewFeed> PostNewFeeds { get; set; }
        public DbSet<ArticalCredit> ArticalCredits { get; set; }

        //Classroom
        //User
        public DbSet<Room> Rooms { get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<StudentRoom> StudentRooms { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //public DbSet<TeacherClass> TeacherClasses { get; set; }
        ////Class
        //public DbSet<Exercise> Exercises { get; set; }
        //public DbSet<Lesson> Lessons { get; set; }

        //Library
        public DbSet<LibraryArticalClass> LibraryArticalClasses { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
