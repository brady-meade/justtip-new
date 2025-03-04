using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using JustTipApp.Models;

namespace JustTipApp.Controllers
{
    //handles all the business related ops
	public class BusinessController : Controller
	{

        public ActionResult Index()
        {
           //gives a list of all businesses
            var businesses = BusinessRepo.GetAll();
            return View(businesses);
        }

        //shows the form to create a new business
        public ActionResult Create()
        {
            return View();
        }

        //form submission for creating a new business
        [HttpPost]
        public ActionResult Create(string businessName)
        {
            if (string.IsNullOrWhiteSpace(businessName))
            {
                ViewBag.Error = "business name cant be empty";
                return View();
            }

            var business = new Business(businessName);
            BusinessRepo.Add(business);
            return RedirectToAction("Index");
        }

        //displays the managesment dashboard for a specific business
        public ActionResult Manage(Guid id)
        {
            var business = BusinessRepo.GetById(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        public ActionResult AddEmployee(Guid id)
        {
            var business = BusinessRepo.GetById(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = id;
            return View();
        }

        //form submission for adding an employee to a business
        [HttpPost]
        public ActionResult AddEmployee(Guid id, string employeeName)
        {
            var business = BusinessRepo.GetById(id);
            if (business == null)
            {
                return HttpNotFound();
            }

            try
            {
                business.AddEmployee(employeeName);
                ViewBag.Message = $"employee '{employeeName}' added";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.BusinessId = id;
            return View();
        }
        
        public ActionResult AssignShift(Guid id)
        {
            var business = BusinessRepo.GetById(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = id;
            ViewBag.employeeName = business.Employees.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Name
            }).ToList();

            return View();
        }

        //form submission for assigning a shift to an employee
        [HttpPost]
        public ActionResult AssignShift(Guid id, string employeeName, string shiftDate)
        {
            var business = BusinessRepo.GetById(id);
            if (business == null)
            {
                return HttpNotFound();
            }

            try
            {
                business.AssignShift(employeeName, shiftDate);
                ViewBag.Message = $"Shift assigned to '{employeeName}' on {shiftDate}.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.BusinessId = id;
            ViewBag.employeeName = business.Employees.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Name
            }).ToList();

            return View();
        }

        public ActionResult DistributeTips(Guid id)
        {
            ViewBag.BusinessId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DistributeTips(Guid id, decimal totalTips)
        {
            var business = BusinessRepo.GetById(id);
            if (business == null)
            {
                return HttpNotFound();
            }

            var tipDistribution = business.DistributeTips(totalTips);
            return View(tipDistribution);
        }
    }
}