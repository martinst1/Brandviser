using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Designer.Models
{
    public class PendingDesignDomainViewModel
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public bool HasLogoUrl { get; set; }
    }
}