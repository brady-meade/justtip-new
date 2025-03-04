using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTipApp.Models
{
	public class Employee
	{
        public string Name { get; private set; }

        public Employee(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Employee name cannot be empty.");
            Name = name;
        }
    }
}