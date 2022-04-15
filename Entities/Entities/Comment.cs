using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class Comment
    {
        public int Id { get; set; }

        public string ContentMember { get; set; }
        public DateTime Create_At { get; set; }

        // FK
        public int? IdMember { get; set; }
        public virtual Member Member { get; set; }
        public int? IdTaskMember { get; set; }
        public virtual TaskMember TaskMember { get; set; }


    }
}
