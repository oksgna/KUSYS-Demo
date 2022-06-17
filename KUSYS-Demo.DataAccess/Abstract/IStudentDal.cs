using KUSYS_Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.DataAccess.Abstract
{
    public interface IStudentDal : IRepository<Student>
    {
        Student GetStudentDetails(int id);
        Student GetStudentDetailsFourUsers(string email);
      List<Student> GetStudentByCourse(string course);
        Student GetByIdWithCourses(int id);
        void Update(Student entity, int[] courseIds);
    }
}
