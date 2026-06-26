using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.ViewModels;

namespace MVCProject.Controllers
{
    public class InstructorController : Controller
    {
        StepsContext context = new StepsContext();
        public IActionResult Index()
        {
            List<Instructor> InsList = context.Instructors.ToList();

            return View("Index", InsList);
        }

        public IActionResult DetailsVM(int id)
        {
            //Instructor instructor = context.Instructors.FirstOrDefault(x => x.Id == id);
            var instructor = context.Instructors
            .Include(i => i.Course)
            .Include(i => i.Department)
            .FirstOrDefault(i => i.Id == id);

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
                context.Instructors.Add(insfromrequest);
                context.SaveChanges();

                return RedirectToAction(actionName: "Index", controllerName: "Instructor");
            }

            return View("New", insfromrequest);

        }


    }
}