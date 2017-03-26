using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Buyer.Models
{
    public class OwnedDomainViewModel
    {
        public string Name { get; set; }

        public DateTime BoughtOn { get; set; }

        public decimal? Price { get; set; }

        public string Seller { get; set; }

        public string Designer { get; set; }
    }
}