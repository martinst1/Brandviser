using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Admin.Models
{
    public class AdminProfileBoxStatsViewModel
    {
        public string FullName { get; set; }

        public string Initials { get; set; }

        public DateTime MemberSince { get; set; }

        public int DomainsPendingApproval { get; set; }

        public int DomainsPendingLogoApproval { get; set; }

    }
}