using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Interface;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels;
using System.Collections;

namespace MVCProject.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepository crsRepo;
        IInstructorRepository insRepo;
        IDepartmentRepository deptRepo;

        public CourseController(ICourseRepository crsrepo, IInstructorRepository insrepo, IDepartmentRepository deptrepo)
        {
            crsRepo = crsrepo;
            insRepo = insrepo;
            deptRepo = deptrepo;
        }

        public IActionResult Index(string searchText)
        {
            var courses = crsRepo.GetAll();

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

            model.InstructorList= insRepo.GetAll();
            model.DeptList= deptRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveNew(CrsInfoExtraInfoViewModel crsfromreq)
        {
            if (ModelState.IsValid == true)
            {
                Course course = new Course()
                {
                    Name = crsfromreq.CrsName,
                    Degree = crsfromreq.CrsDegree,
                    MinDegree = crsfromreq.CrsMinDegree,
                    Hours = crsfromreq.CrsHours,
                    Dept_Id = crsfromreq.DepartmentId
                };

                Instructor instructor = insRepo.GetById(crsfromreq.InstructorId);

                if (instructor != null)
                {
                    course.Instructors.Add(instructor);
                }

                crsRepo.Add(course);
                crsRepo.Save();

                return RedirectToAction("Index", "Course");
            }

            crsfromreq.DeptList= deptRepo.GetAll() ;
            crsfromreq.InstructorList= insRepo.GetAll() ;

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
            Course course = crsRepo.GetById(id);

            if (course == null)
                return NotFound();

            List<Department> deptlist = deptRepo.GetAll();
            List<Instructor> inslist = insRepo.GetAll();

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
                if (crsRepo.GetById(crsfromreq.CrsId) == null)
                {
                    return NotFound();
                }

                Course course = new Course()
                {
                    Id = crsfromreq.CrsId,
                    Name = crsfromreq.CrsName,
                    Degree = crsfromreq.CrsDegree,
                    MinDegree = crsfromreq.CrsMinDegree,
                    Hours = crsfromreq.CrsHours,
                    Dept_Id = crsfromreq.DepartmentId
                };

                var instructor = insRepo.GetById(crsfromreq.InstructorId);

                if (instructor != null)
                {
                    course.Instructors.Add(instructor);
                }

                crsRepo.Update(course);
                crsRepo.Save();

                return RedirectToAction("Index", "Course");

            }
            crsfromreq.CrsId = id;
            crsfromreq.DeptList= deptRepo.GetAll();
            crsfromreq.InstructorList= insRepo.GetAll();

            return View("Edit", crsfromreq);
        }

    }
}
