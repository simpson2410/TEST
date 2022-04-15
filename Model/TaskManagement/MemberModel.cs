using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MemberModel
    {
		public string Name { get; set; }
		public string Status { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Specialist { get; set; }
		public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string IdRegion { get; set; }
	}

	public class MemberModelUp
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Specialist { get; set; }
		public DateTime Update_At { get; set; }
		public string IdRegion { get; set; }
	}
}
