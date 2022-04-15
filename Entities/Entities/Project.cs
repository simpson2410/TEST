using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public partial class Project
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public double ProjectCost { get; set; }
		public double TeamSize { get; set; }
		public string Action { get; set; }
		public string CurrentExpenditure { get; set; }
		public string AvailableFunds { get; set; }
		public DateTime Start_At { get; set; }
		public DateTime End_At { get; set; }

		// Foreign Key
		public int IdClient { get; set; }
		public virtual Client Client { get; set; }	

		// ---
		public virtual ProjectDetail ProjectDetail { get; set; }
		public virtual ICollection<TaskMember> TaskMembers { get; set; }
	
	}
}
