using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Buyer.Models
{
    public class BuyerProfileBoxStatsViewModel
    {
        public string FullName { get; set; }

        public string Initials { get; set; }

        public DateTime MemberSince { get; set; }

        public string BalanceInKUsd { get; set; }

        public decimal Balance { get; set; }

        public int OwnedDomains { get; set; }
    }
}