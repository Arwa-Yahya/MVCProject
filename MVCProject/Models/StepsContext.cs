using Microsoft.EntityFrameworkCore;
namespace MVCProject.Models
{
    public class StepsContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public StepsContext(DbContextOptions <StepsContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrsResult>().HasKey(c => new { c.Crs_Id, c.Trainee_Id });
        }

    }
}
