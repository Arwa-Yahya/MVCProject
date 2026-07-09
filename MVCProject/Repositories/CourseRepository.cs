using Microsoft.EntityFrameworkCore;
using MVCProject.Interface;
using MVCProject.Models;

namespace MVCProject.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        StepsContext context;

        public CourseRepository(StepsContext _context)
        {
            context = _context;
        }

        public List<Course> GetAll()
        {
            return context.Courses
                  .Include(c => c.Instructors)
                  .Include(c => c.Department)
                  .ToList();
        }

        public Course GetById(int id)
        {
            return context.Courses
                   .Include(c => c.Instructors)
                   .Include(c => c.Department)
                   .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course entity)
        {
            context.Courses.Add(entity);
        }

        public void Update(Course entity)
        {
            Course crsdb = GetById(entity.Id);

            if (crsdb != null)
            {
                crsdb.Name = entity.Name;
                crsdb.Degree = entity.Degree;
                crsdb.MinDegree = entity.MinDegree;
                crsdb.Hours = entity.Hours;
                crsdb.Dept_Id = entity.Dept_Id;
                crsdb.Instructors = entity.Instructors;
            }

        }

        public void Delete(int id)
        {
            Course crs = GetById(id);

            if (crs != null)
            {
                context.Courses.Remove(crs);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

      
    }
}
