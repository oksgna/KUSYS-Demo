using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.Entities;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.WebUI.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private IStudentService _studentService;

        private ICourseService _courseService;

        public AdminController(IStudentService studentService, ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService; 
        }
        public IActionResult Index()
        {

            return View(new StudentListModel()
            {
                Students = _studentService.GetAll()
            });

          

        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentModel model)
        {
            var entity = new Student()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Email = model.Email
               
            };

            _studentService.Create(entity);

            return RedirectToAction("Index");
        }



        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _studentService.GetByIdWithCourses((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new StudentModel()
            {
                StudentId = entity.StudentId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                SelectedStudentCourse = entity.StudentCourse.Select(i => i.Course).ToList()
            };
            ViewBag.Courses = _courseService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(StudentModel model,int[] courseIds)
        {
            var entity = _studentService.GetById(model.StudentId);

            if (entity == null)
            {
                return NotFound();
            }

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.BirthDate = model.BirthDate;

            _studentService.Update(entity,courseIds);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int studentId)
        {
            var entity = _studentService.GetById(studentId);

            if (entity != null)
            {
                _studentService.Delete(entity);
            }

            return RedirectToAction("Index");
        }


    }
}
