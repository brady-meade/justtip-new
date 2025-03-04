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

		public void AssignShift(Employee employee, string date)
		{
			if (!DateTime.TryParse(date, out _)) throw new ArgumentException("not a valid date format");
			if (!Schedule.ContainsKey(employee)) Schedule[employee] = new List<String>();

			Schedule[employee].Add(date);
		}

        public Dictionary<Employee, int> GetWorkDays()
        {
            return Schedule.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Count);
        }
    }
}