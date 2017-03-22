using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Seller.Models
{
    public class ValidateDomainViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VerificationCode { get; set; }
    }
}