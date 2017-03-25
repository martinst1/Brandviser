using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Admin.Models
{
    public class PendingLogoApprovalDomainViewModel : PendingApprovalDomainViewModel
    {
        public string LogoUrl { get; set; }
    }
}