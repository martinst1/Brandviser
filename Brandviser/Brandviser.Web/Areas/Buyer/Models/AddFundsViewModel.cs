using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Buyer.Models
{
    public class AddFundsViewModel
    {
        [Required]
        [Range(1, 100000, ErrorMessage = "You can add minimum $1 and maximum $100000")]
        public decimal Amount { get; set; }
    }
}