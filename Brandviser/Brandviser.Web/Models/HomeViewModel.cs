using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Models
{
    public class HomeViewModel
    {
        public string SearchBoxText { get; set; }

        public IEnumerable<DomainViewModel> Domains { get; set; }
    }
}