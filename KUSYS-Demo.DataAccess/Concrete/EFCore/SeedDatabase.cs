using KUSYS_Demo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.DataAccess.Concrete.EFCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new KUSYSContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Students.Count() == 0)
                {
                    context.Students.AddRange(Students);
                   
                }

                if (context.Courses.Count() == 0)
                {
                    context.Courses.AddRange(Courses);
                    context.AddRange(StudentsCourses);
                }

                context.SaveChanges();
            }
        }

        private static Student[] Students =     {
            new Student() { FirstName="Ali", LastName="Yılmaz", Email="mail@mail.com"},
            new Student() { FirstName="Ayşe", LastName="Öz",Email="mail@mail.com"},
            new Student() { FirstName="Ahmet", LastName="ÖzYılmaz", Email = "mail@mail.com" },
            new Student() { FirstName="Elif", LastName="Kök", Email = "mail@mail.com"   },
            new Student() { FirstName="Zeynep", LastName="Çalışkan", Email = "mail@mail.com"    },
            new Student() { FirstName="Hakan", LastName="Taş", Email = "mail@mail.com"  },
            new Student() { FirstName="Hüseyin", LastName="Kara", Email = "mail@mail.com"   }
        };

        private static Course[] Courses =
        {
            new Course(){ CourseName="Calculus"},
            new Course(){ CourseName="Physics"},
            new Course(){ CourseName="Algorithms"},
            new Course(){ CourseName="Introduction to Computer Science"}

           
        };

        private static StudentCourse[] StudentsCourses =
        {
            new StudentCourse(){Student=Students[0],Course=Courses[0] },
            new StudentCourse(){Student=Students[0],Course=Courses[1] },
            new StudentCourse(){Student=Students[0],Course=Courses[2] },
            new StudentCourse(){Student=Students[0],Course=Courses[3] },
            new StudentCourse(){Student=Students[1],Course=Courses[1] },
            new StudentCourse(){Student=Students[1],Course=Courses[0] },
            new StudentCourse(){Student=Students[2],Course=Courses[2] },
            new StudentCourse(){Student=Students[2],Course=Courses[3] },


        };
    }
}

