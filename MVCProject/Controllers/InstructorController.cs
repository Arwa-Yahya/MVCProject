using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Interface;
using MVCProject.Models;
using MVCProject.ViewModels;

namespace MVCProject.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepository insRepo;
        IDepartmentRepository deptRepo;

        public InstructorController(IInstructorRepository insrepo, IDepartmentRepository deptrepo)
        {
            insRepo = insrepo;
            deptRepo = deptrepo;
        }

        public IActionResult Index()
        {
            List<Instructor> InsList = insRepo.GetAll();

            return View("Index", InsList);
        }

        public IActionResult DetailsVM(int id)
        {
            //Instructor instructor = context.Instructors.FirstOrDefault(x => x.Id == id);
            Instructor instructor = insRepo.GetById(id);

            if (instructor == null)
            {
                return NotFound();
            }

            InsInfoExtraInfoViewModel InsVM = new InsInfoExtraInfoViewModel()
            {
                InsId = instructor.Id,
                InsName = instructor.Name,
                InsImageUrl = instructor.ImageUrl,
                InsSalary = instructor.Salary,
                InsAddress = instructor.Address,
                CrsName = instructor.Course.Name,
                DeptName = instructor.Department.Name

            };

            return View("DetailsVM", InsVM);
        }

        public IActionResult New()
        {
            return View("New");
        }

        public IActionResult SaveNew(Instructor insfromrequest)
        {
            if (insfromrequest.Name != null)
            {
                insRepo.Add(insfromrequest);
                insRepo.Save();

                return RedirectToAction(actionName: "Index", controllerName: "Instructor");
            }

            return View("New", insfromrequest);

        }


    }
}