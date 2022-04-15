using Business.IRepostitory;
using Entities.DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Repository
{
   
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(TNRContext context) : base(context)
        {

        }

    }

    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(TNRContext context) : base(context)
        {

        }

    }
}
