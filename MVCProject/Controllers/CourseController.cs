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
        public IActionResult Index(string searchText)
        {
            var courses = context.Courses.Include(i => i.Instructors).Include(d => d.Department).ToList();

            if (searchText != null)
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    ViewBag.Msg = "Please enter a search value";
                }
                else
                {
                    courses = courses.Where(c => c.Name.Contains(searchText)).ToList();

                    if (courses.Count == 0)
                    {
                        ViewBag.Msg = "No courses found";
                    }
                }
            }

            return View(courses);
        }
        public IActionResult New()
        {
            CrsInfoExtraInfoViewModel model = new CrsInfoExtraInfoViewModel();

            model.InstructorList= context.Instructors.ToList();
            model.DeptList= context.Departments.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveNew(CrsInfoExtraInfoViewModel crsfromreq)
        {
            if (ModelState.IsValid == true)
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

            crsfromreq.DeptList= context.Departments.ToList();
            crsfromreq.InstructorList= context.Instructors.ToList();

            return View("New", crsfromreq);
        }

        public IActionResult CheckMinDegree(int CrsDegree, int CrsMinDegree)
        {
            if (CrsMinDegree > CrsDegree) return Json("MinDegree must be less than Degree");

            else return Json(true);
        }

        public IActionResult DividedBy3(int CrsHours)
        {
            //if (CrsHours == null) return Json("Hours is required");
            if (CrsHours % 3 != 0) return Json("CrsHours must be Divided By 3");

            else return Json(true);
        }

        public IActionResult Edit(int id)
        {
            Course course = context.Courses.Include(c => c.Instructors).FirstOrDefault(e => e.Id == id);

            if (course == null)
                return NotFound();

            List<Department> deptlist = context.Departments.ToList();
            List<Instructor> inslist = context.Instructors.ToList();

            CrsInfoExtraInfoViewModel model = new CrsInfoExtraInfoViewModel()
            {

                CrsId = course.Id,
                CrsName = course.Name,
                CrsDegree = course.Degree,
                CrsMinDegree = course.MinDegree,
                CrsHours = course.Hours,
                DepartmentId = course.Dept_Id,
                InstructorId = course.Instructors?.FirstOrDefault()?.Id ?? 0,
                DeptList= deptlist,
                InstructorList= inslist
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveEdit(CrsInfoExtraInfoViewModel crsfromreq, int id)
        {
            if (ModelState.IsValid)
            {
                Course crsfromdb = context.Courses.FirstOrDefault(e => e.Id == crsfromreq.CrsId);

                if (crsfromdb == null) return NotFound();

                crsfromdb.Name = crsfromreq.CrsName;
                crsfromdb.Degree = crsfromreq.CrsDegree;
                crsfromdb.MinDegree = crsfromreq.CrsMinDegree;
                crsfromdb.Hours = crsfromreq.CrsHours;
                crsfromdb.Dept_Id = crsfromreq.DepartmentId;
                var instructor = context.Instructors.FirstOrDefault(i => i.Id == crsfromreq.InstructorId);

                if (instructor != null) instructor.Crs_Id = crsfromdb.Id;

                context.SaveChanges();
                return RedirectToAction("Index", "Course");

            }
            crsfromreq.CrsId = id;
            crsfromreq.DeptList= context.Departments.ToList();
            crsfromreq.InstructorList= context.Instructors.ToList();

            return View("Edit", crsfromreq);
        }

    }
}
