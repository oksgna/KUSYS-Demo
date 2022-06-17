using KUSYS_Demo.Entities;

namespace KUSYS_Demo.WebUI.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Course> SelectedStudentCourse { get; set; }
    }
}
