using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.DataAccess.Abstract;
using KUSYS_Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.Concrete
{
    public class CourseManager : ICourseService
    {

        private ICourseDal _courseDal;
        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;

        }
        public void Create(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Course entity)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAll()
        {
            return _courseDal.GetAll();
        }

        public Course GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
