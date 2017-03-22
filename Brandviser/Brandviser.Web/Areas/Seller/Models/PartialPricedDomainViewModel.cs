using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Seller.Models
{
    public class PartialPricedDomainViewModel : PartialDomainViewModel
    {
        public decimal? Price { get; set; }
    }
}