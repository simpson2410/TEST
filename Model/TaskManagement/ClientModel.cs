using System;
using System.Collections.Generic;
using System.Text;

namespace Model.TaskManagement
{
    public class ClientModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Create_At { get; set; }
    }

    public class ClientModel1
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Update_At { get; set; }
    }
}
