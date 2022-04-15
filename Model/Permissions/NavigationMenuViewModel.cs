﻿using System;
using System.Collections.Generic;

namespace Model.Permissions
{
	public class NavigationMenuViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public Guid? ParentMenuId { get; set; }

		public string Area { get; set; }

		public string ControllerName { get; set; }

		public string ActionName { get; set; }

		public bool IsExternal { get; set; }

		public string ExternalUrl { get; set; }

		public bool Permitted { get; set; }

		public int DisplayOrder { get; set; }

		public bool Visible { get; set; }
		public bool IsMenu { get; set; }
		public string RoleName { get; set; }
		public string RoleId { get; set; }
		public string Image { get; set; }
	}
}