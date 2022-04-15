using Business.IRepository;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.IRepostitory
{
    public interface IStudentRepository : IRepository<Student>
    {

    }

    public interface IGradeRepository : IRepository<Grade>
    {

    }
}
