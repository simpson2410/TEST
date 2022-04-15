using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CurrentGradeId { get; set; }
        public Grade Grade { get; set; }

    }

    public class Grade
    {
        public Grade()
        {
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
