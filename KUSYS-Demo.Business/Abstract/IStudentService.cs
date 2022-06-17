using KUSYS_Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.Abstract
{
    public interface IStudentService
    {
        Student GetById(int id);
        Student GetStudentDetails(int id);
        Student GetStudentDetailsFourUsers(string email);
        List<Student> GetStudentByCourse(string course);
        List<Student> GetAll();
        void Create(Student entity);
        void Update(Student entity);
        void Delete(Student entity);
        Student GetByIdWithCourses(int id);
        void Update(Student entity, int[] courseIds);
    }
}
