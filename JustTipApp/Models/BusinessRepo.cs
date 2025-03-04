using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTipApp.Models
{
	public class BusinessRepo
	{
        private static List<Business> _businesses = new List<Business>();

        public static IEnumerable<Business> GetAll()
        {
            return _businesses;
        }

        public static Business GetById(Guid id)
        {
            return _businesses.FirstOrDefault(b => b.id == id);
        }

        public static void Add(Business business)
        {
            _businesses.Add(business);
        }
    }
}