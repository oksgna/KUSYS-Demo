using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.DataAccess.Abstract;
using KUSYS_Demo.DataAccess.Concrete.EFCore;
using KUSYS_Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private IStudentDal _studentDal;
        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }


        public void Create(Student entity)
        {
            _studentDal.Create(entity);

        }

        public void Delete(Student entity)
        {
            _studentDal.Delete(entity);
        }

        public List<Student> GetAll()
        {
            return _studentDal.GetAll();
        }



        public Student GetById(int id)
        {
            return _studentDal.GetById(id);
        }

        public Student GetByIdWithCourses(int id)
        {
            return _studentDal.GetByIdWithCourses(id);
        }

        public List<Student> GetStudentByCourse(string course)
        {
            return _studentDal.GetStudentByCourse(course);
        }

        public Student GetStudentDetails(int id)
        {
            return _studentDal.GetStudentDetails(id);
        }

        public Student GetStudentDetailsFourUsers(string email)
        {
            return _studentDal.GetStudentDetailsFourUsers(email);
        }
        public void Update(Student entity)
        {
             _studentDal.Update(entity);
        }

        public void Update(Student entity, int[] courseIds)
        {
            _studentDal.Update(entity, courseIds);
        }
    }
}
