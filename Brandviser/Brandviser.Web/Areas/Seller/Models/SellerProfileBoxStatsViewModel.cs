using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Seller.Models
{
    public class SellerProfileBoxStatsViewModel
    {
        public string FullName { get; set; }

        public string Initials { get; set; }

        public DateTime MemberSince { get; set; }

        public string BalanceInKUsd { get; set; }

        public decimal Balance { get; set; }

        public int SubmittedDomains { get; set; }

        public int RejectedDomains { get; set; }

        public int PendingDomains { get; set; }

        public int PublishedDomains { get; set; }

        public int SoldDomains { get; set; }
    }
}