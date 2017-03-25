using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Admin.Models
{
    public class ApproveDomainNameViewModel
    {
        public int Id { get; set; }

        [Display(Name="Domain")]
        public string Name { get; set; }

        [Display(Name = "Name")]
        public string SellerName { get; set; }

        [Display(Name = "Username")]
        public string SellerUsername { get; set; }

        public decimal? Price { get; set; }
    }
}