
using System;
using System.Collections.Generic;
using System.Text;
namespace Model.TaskManagement
{
	public class CommentModel
	{
        public string ContentMember { get; set; }
        public DateTime Create_At { get; set; }

        // FKey
        public int? IdMember { get; set; }
        public int? IdTaskMember { get; set; }
    }
    public class CommentModel1
    {
        public int Id { get; set; }
        public string ContentMember { get; set; }

        // FKey
        public int? IdMember { get; set; }
        public int? IdTaskMember { get; set; }
    }
}

