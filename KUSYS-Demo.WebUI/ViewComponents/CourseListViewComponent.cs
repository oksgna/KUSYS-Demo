using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.WebUI.ViewComponents

{
    public class CourseListViewComponent : ViewComponent
    {
        private ICourseService _courseService;
        public CourseListViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IViewComponentResult Invoke()
        {

            return View(new CourseListViewModel()
            {
                SelectedCourse = RouteData.Values["course"]?.ToString(),
                Courses = _courseService.GetAll()
            });


        }

    }
}
