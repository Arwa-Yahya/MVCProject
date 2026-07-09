using Microsoft.AspNetCore.Mvc;
using MVCProject.Interface;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository deptRepo;

        public DepartmentController(IDepartmentRepository _deptRepo)
        {
            deptRepo = _deptRepo;
        }

        public IActionResult Index()
        {
            List<Department> departments = deptRepo.GetAll();

            return View("Index", departments);
        }

        public IActionResult Edit(int id)
        {
            Department department = deptRepo.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            return View("Edit", department);
        }

        [HttpPost]
        public IActionResult SaveEdit(Department dept)
        {
            if (ModelState.IsValid)
            {
                if (deptRepo.GetById(dept.Id) == null)
                {
                    return NotFound();
                }

                deptRepo.Update(dept);
                deptRepo.Save();

                return RedirectToAction("Index","Department");
            }

            return View("Edit", dept);
        }
    }
}