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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ARWA\\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrsResult>().HasKey(c => new { c.Crs_Id, c.Trainee_Id });
        }

    }
}
