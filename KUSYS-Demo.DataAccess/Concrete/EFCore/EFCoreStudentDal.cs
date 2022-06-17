using KUSYS_Demo.DataAccess.Abstract;
using KUSYS_Demo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.DataAccess.Concrete.EFCore
{
    public class EFCoreStudentDal : EFCoreGenericRepository<Student, KUSYSContext>, IStudentDal
    {
        public Student GetByIdWithCourses(int id)
        {
           using(var context = new KUSYSContext())
            {
                return context.Students
                    .Where(i => i.StudentId == id)
                    .Include(i=>i.StudentCourse)
                    .ThenInclude(i=>i.Course)
                    .FirstOrDefault();
            }
        }

        public List<Student> GetStudentByCourse(string course)
        {
            using (var context = new KUSYSContext())
            {
                var students = context.Students.AsQueryable();

                if (!string.IsNullOrEmpty(course))
                {
                    students = students
                                .Include(i => i.StudentCourse)
                                .ThenInclude(i => i.Course)
                                .Where(i => i.StudentCourse.Any(a => a.Course.CourseName.ToLower() == course.ToLower()));
                }
                return students.ToList();
            }

        }

        public Student GetStudentDetails(int id)
        {
            using (var context = new KUSYSContext())
            {
                return context.Students
                    .Where(i => i.StudentId == id)
                    .Include(i => i.StudentCourse)
                    .ThenInclude(i => i.Course)
                    .FirstOrDefault();
            }
        }
        public Student GetStudentDetailsFourUsers(string email)
        {
            using (var context = new KUSYSContext())
            {
                return context.Students
                    .Where(i => i.Email == email)
                    .Include(i => i.StudentCourse)
                    .ThenInclude(i => i.Course)
                    .FirstOrDefault();
            }
        }


        public void Update(Student entity, int[] courseIds)
        {
           using(var context = new KUSYSContext())
            {
                var student = context.Students
                    .Include(i => i.StudentCourse)
                    .FirstOrDefault(i => i.StudentId == entity.StudentId);



                if (student != null)
                {
                    student.FirstName = entity.FirstName;
                    student.LastName = entity.LastName;
                    student.BirthDate = entity.BirthDate;
                    student.StudentCourse = courseIds.Select(courseid => new StudentCourse()
                    {
                        CourseId = courseid,
                        StudentId = entity.StudentId
                    }).ToList();

                    context.SaveChanges();
                }

            }
        }
    }
}
