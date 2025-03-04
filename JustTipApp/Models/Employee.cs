using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTipApp.Models
{
	public class Employee
	{
        public string Name { get; private set; }
        private List<string> Shifts { get; set; }

        public Employee(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Employee name cannot be empty.");
            Name = name;
            Shifts = new List<string>();
        }

        public void AddShift(string date)
        {
            if (!DateTime.TryParse(date, out _)) throw new ArgumentException("wrong date format");

            Shifts.Add(date);
        }

        public int GetShifts()
        {
            return Shifts.Count;
        }
    }
}