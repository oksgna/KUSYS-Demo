using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.Entities;
using KUSYS_Demo.WebUI.Identity;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace KUSYS_Demo.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IStudentService _studentService;
        private ICourseService _courseService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public UserController(IStudentService studentService, ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService; 
        }

        public IActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            Student student = _studentService.GetStudentDetails((int)id);
            if (student == null)
            {
                return NotFound();
            }
            return View(new StudentDetailsModel()
            {
                Student = student,  
                Courses= student.StudentCourse.Select(i=>i.Course).ToList()
            });
           
        }
        public IActionResult DetailsForUser()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email);
            string role = User.FindFirstValue(ClaimTypes.Role);
            if (email == null)
            {
                return NotFound();
            }
            Student student = _studentService.GetStudentDetailsFourUsers((string)email);
            if (student == null)
            {
                return NotFound();
            }
            return View(new StudentDetailsModel()
            {
                Student = student,
                Courses = student.StudentCourse.Select(i => i.Course).ToList()
            });

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
        public IActionResult Edit(StudentModel model, int[] courseIds)
        {
            var entity = _studentService.GetById(model.StudentId);

            if (entity == null)
            {
                return NotFound();
            }

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.BirthDate = model.BirthDate;

            _studentService.Update(entity, courseIds);

            return RedirectToAction("DetailsForUser","user");
        }

        public IActionResult List(string course)
        {
            return View(new StudentListModel()
            {
                Students = _studentService.GetStudentByCourse(course)
            }) ; 
        }
    }

}
