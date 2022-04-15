using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime Update_At { get; set; }

        public virtual ICollection<Member> Members { get; set; }

    }
}
