using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Seller.Models
{
    public class PartialPricedDatedDomainViewModel : PartialPricedDomainViewModel
    {
        public DateTime SoldOn { get; set; }
    }
}