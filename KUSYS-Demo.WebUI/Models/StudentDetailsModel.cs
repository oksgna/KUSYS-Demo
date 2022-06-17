using KUSYS_Demo.Entities;

namespace KUSYS_Demo.WebUI.Models
{
    public class StudentDetailsModel
    {
        public Student Student { get; set; }
        public List<Course> Courses { get; set; }
    }
}
