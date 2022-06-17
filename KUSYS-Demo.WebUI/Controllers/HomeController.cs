using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private IStudentService _studentService;
        public HomeController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public IActionResult Index()
        {
            return View(new StudentListModel()
            {
                Students = _studentService.GetAll()
            }); 
        }
    }
}
