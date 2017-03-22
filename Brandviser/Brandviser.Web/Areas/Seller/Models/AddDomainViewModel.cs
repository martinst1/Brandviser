using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Seller.Models
{
    public class AddDomainViewModel
    {
        [Required]
        [StringLength(259)]
        [MinLength(4)]
        [RegularExpression("^([a-z0-9]+(-[a-z0-9]+)*\\.)+[.com]{3,3}$", ErrorMessage = "Only valid .com domains are accepted.")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [MinLength(10)]
        public string Description { get; set; }
    }
}