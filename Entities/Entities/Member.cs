using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class Member
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Specialist { get; set; }
		public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }

		// Foreign Key
		public int IdRegion { get; set; }
		public virtual Region Region { get; set; }

		// ---
		public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
		public virtual ICollection<TaskMember> TaskMembers { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
	}
}
