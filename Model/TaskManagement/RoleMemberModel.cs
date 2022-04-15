using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RoleMemberModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime Update_At { get; set; }
    }
    public class RoleMemberModelUp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Update_At { get; set; }
    }
}
