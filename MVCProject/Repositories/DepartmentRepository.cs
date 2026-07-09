using MVCProject.Interface;
using MVCProject.Models;

namespace MVCProject.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        StepsContext context;
        public DepartmentRepository(StepsContext _context)
        {
            context = _context;
        }
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Department entity)
        {
            context.Departments.Add(entity);
        }

        public void Update(Department entity)
        {
            Department dept = GetById(entity.Id);

            if (dept != null)
            {
                dept.Name = entity.Name;
                dept.Manager = entity.Manager;
            }
        }

        public void Delete(int id)
        {
            Department dept = GetById(id);

            if (dept != null)
            {
                context.Departments.Remove(dept);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}