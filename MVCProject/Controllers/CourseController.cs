using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.ViewModels;
using System.Collections;

namespace MVCProject.Controllers
{
    public class CourseController : Controller
    {
        StepsContext context = new StepsContext();
        public IActionResult Index()
        {
            var courses = context.Courses
                 .Include(i => i.Instructors)
                 .ToList();

            return View(courses);
        }

        public IActionResult New()
        {
            CrsInfoExtraInfoViewModel model = new CrsInfoExtraInfoViewModel();

            model.InstructorList = context.Instructors.ToList();
            model.DeptList = context.Departments.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveNew(CrsInfoExtraInfoViewModel crsfromreq)
        {
            if (crsfromreq.CrsName != null)
            {
                Course course = new Course();

                course.Name = crsfromreq.CrsName;
                course.Degree = crsfromreq.CrsDegree;
                course.MinDegree = crsfromreq.CrsMinDegree;
                course.Hours = crsfromreq.CrsHours;
                course.Dept_Id = crsfromreq.DepartmentId;
                course.Instructors = context.Instructors
                .Where(x => x.Id == crsfromreq.InstructorId)
               .ToList();

                context.Courses.Add(course);
                context.SaveChanges();

                return RedirectToAction("Index", "Course");
            }

            return View("New", crsfromreq);
        }
    }
}
