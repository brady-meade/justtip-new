using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace JustTipApp.Models
{
	public class Business
	{
        //guid as a unique identifier for a business object
        public Guid id { get; private set; }
        //name of the business
        public string Name { get; private set; }
        //list of employee objects associated with business
        public List<Employee> Employees { get; private set; }
        //roster for employee shifts
        private Roster Roster { get; set; }

        //initialises business class instance
        public Business(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("business name cannot be empty");
            id = Guid.NewGuid();// new unique identifier
            Name = name;
            Employees = new List<Employee>();// new list of employees
            Roster = new Roster();// new shift roster
        }

        //add new employee class instance
        public void AddEmployee(string employeeName)
        {
            //ensure the employee doesnt already exist
            if (Employees.Any(e => e.Name == employeeName)) throw new Exception("this employee already exists");
            Employees.Add(new Employee(employeeName));
        }

        //assigns shift to an employee for a specific date
        public void AssignShift(string employeeName, string date)
        {
            //checks if the employee exists
            var employee = Employees.FirstOrDefault(e => e.Name == employeeName);
            if (employee == null) throw new Exception($"employee {employeeName} not found");

            //assigns the shift to the employee
            employee.AddShift(date);
            Roster.AssignShift(employee, date);
        }

        //distributes the tips among the employees based on the number of shifts worked.
        public Dictionary<string, decimal> DistributeTips(decimal totalTips)
        {
            if (Employees.Count == 0)
                throw new Exception("cant distribute tips if no employees in the business");
            //retrieve the total workdays from all employees (tronc tokens if you will)
            var workDays = Roster.GetWorkDays();
            int totalWorkedDays = workDays.Values.Sum();

            //if there were no days worked then it just give an empty distribution
            if (totalWorkedDays == 0) return new Dictionary<string, decimal>();

            var tipDistribution = new Dictionary<string, decimal>();

            foreach (var employee in Employees)
            {
                tipDistribution[employee.Name] = 0m;
            }

            foreach (var entry in workDays)
            {
                Employee employee = entry.Key;
                int daysWorked = entry.Value;
                decimal share = (daysWorked / (decimal)totalWorkedDays) * totalTips;
                tipDistribution[employee.Name] = Math.Round(share, 2);
            }
            return tipDistribution;
        }
    }
}