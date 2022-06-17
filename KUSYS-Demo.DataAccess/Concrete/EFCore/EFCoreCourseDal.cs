using KUSYS_Demo.DataAccess.Abstract;
using KUSYS_Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.DataAccess.Concrete.EFCore
{
    public class EFCoreCourseDal : EFCoreGenericRepository<Course,KUSYSContext>,ICourseDal
    {
    }
}
