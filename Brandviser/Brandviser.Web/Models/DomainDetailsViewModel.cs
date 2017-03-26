using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Models
{
    public class DomainDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SellerName { get; set; }

        public string DesignerName { get; set; }

        public decimal? Price { get; set; }

        public string LogoUrl { get; set; }

        public string SellerId { get; set; }

        public DateTime? PostedOn { get; set; }
    }
}