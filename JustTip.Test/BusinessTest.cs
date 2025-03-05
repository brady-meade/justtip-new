using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JustTipApp.Models;

namespace JustTip.Test
{
    [TestClass]
    public class BusinessTest
    {
        [TestMethod]
        public void Should_Not_Distribute_Tips_With_No_Employees()
        {
            Business business = new Business("Petrochem");
            var e = Assert.ThrowsException<Exception>(() => business.DistributeTips(100));
            Assert.AreEqual("cant distribute tips if no employees in the business", e.Message);
        }
        [TestMethod]
        public void Tips_Distributed_Correctly()
        {
            Business business = new Business("Petrochem");
            business.AddEmployee("Angus Youngblood");
            business.AddEmployee("Michiko Arasaka");
            business.AddEmployee("Johnny Silverhand");
            business.AssignShift("Angus Youngblood", "2023-03-01");
            business.AssignShift("Michiko Arasaka", "2023-03-02");
            business.AssignShift("Michiko Arasaka", "2023-03-05");
            business.AssignShift("Johnny Silverhand", "2023-03-04");
            decimal totalTips = 100;
            var distribution = business.DistributeTips(totalTips);

            Assert.AreEqual(50, distribution["Michiko Arasaka"], "Michiko should get 50");
            Assert.AreEqual(25, distribution["Johnny Silverhand"], "Johnny should get 25");
        }

        [TestMethod]
        public void Zero_Day_Employees_Get_no_Tips()
        {
            Business business = new Business("Arasaka");
            business.AddEmployee("Saburo Arasaka");
            business.AddEmployee("Hanako Arasaka");
            business.AddEmployee("Yorinobu Arasaka");
            business.AssignShift("Saburo Arasaka", "2024-01-01");
            business.AssignShift("Hanako Arasaka", "2024-01-02");
            decimal totalTips = 100;
            var distribution = business.DistributeTips(totalTips);

            Assert.AreEqual(50, distribution["Saburo Arasaka"], "Saburo Should get 50");
            Assert.AreEqual(0, distribution["Yorinobu Arasaka"], "Yori should get 0");
        }
    }
}
