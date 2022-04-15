using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class ProjectDetail
    {
        public int Id { get; set; } 

        // Foreign Key
        public int? IdProject { get; set; }
        public virtual Project Project { get; set; }
        public int? IdMember { get; set; }
        public virtual Member Member { get; set; }
        public int? IdRole { get; set; }
        public virtual RoleMember RoleMember { get; set; }
    }
}
