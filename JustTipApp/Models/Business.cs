using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace JustTipApp.Models
{
	public class Business
	{
        public string Name { get; private set; }
        private List<Employee> Employees { get; set; }
        private Roster Roster { get; set; }

        public Business(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Business name cannot be empty.");
            Name = name;
            Employees = new List<Employee>();
            Roster = new Roster();
        }
    }
}