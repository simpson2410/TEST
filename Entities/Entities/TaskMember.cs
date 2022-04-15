using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class TaskMember
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentTask { get; set; }
        public string Create_At { get; set; }
        public string Update_At { get; set; }

        // Foreign Key
        public int? IdProject { get; set; }
        public virtual Project Project { get; set; }
        public int? IdMember { get; set; }
        public virtual Member Member { get; set; }

        // ---
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
