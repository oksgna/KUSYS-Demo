using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Entities
{

    //Çoka çok ilişki sağlayan sınıf
    //(Bir öğrencinin birden fazla kursu olabilir. Bir kursa kayıtlı birden fazla öğrenci olabilir.)
    
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
