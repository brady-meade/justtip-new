using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTipApp.Models
{
	public class Roster
	{
		private Dictionary<Employee, List<string>> Schedule { get; set; }

		public Roster()
		{
			Schedule = new Dictionary<Employee, List<string>>();
		}


	}
}