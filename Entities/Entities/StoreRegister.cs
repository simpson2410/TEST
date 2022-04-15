using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class StoreRegister
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public string Badges { get; set; }
        public string BadgeUrl { get; set; }
        public string BadgeFBShareUrl { get; set; }
        public int Level { get; set; }
    }
}
