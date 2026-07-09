using Microsoft.EntityFrameworkCore;
using MVCProject.Interface;
using MVCProject.Models;

namespace MVCProject.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        StepsContext context;
        public InstructorRepository(StepsContext _context)
        {
            context = _context;
        }
        public List<Instructor> GetAll()
        {
            return context.Instructors
                  .Include(i => i.Course)
                  .Include(i => i.Department)
                  .ToList();
        }

        public Instructor GetById(int id)
        {
            return context.Instructors
                  .Include(i => i.Course)
                 .Include(i => i.Department)
                 .FirstOrDefault(i => i.Id == id);
        }

        public void Add(Instructor entity)
        {
            context.Instructors.Add(entity);
        }

        public void Update(Instructor entity)
        {
            Instructor ins = GetById(entity.Id);

            if (ins != null)
            {
                ins.Name = entity.Name;
                ins.ImageUrl = entity.ImageUrl;
                ins.Salary = entity.Salary;
                ins.Address = entity.Address;
                ins.Crs_Id = entity.Crs_Id;
                ins.Dept_Id = entity.Dept_Id;
            }
        }

        public void Delete(int id)
        {
            Instructor ins = GetById(id);

            if (ins != null)
            {
                context.Instructors.Remove(ins);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}