using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public List<StudentCourse> StudentCourse { get; set; }
    }
}
