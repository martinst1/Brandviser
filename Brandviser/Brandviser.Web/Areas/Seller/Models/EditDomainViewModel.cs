using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brandviser.Web.Areas.Seller.Models
{
    public class EditDomainViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Description should be between 10 and 255 symbols.")]
        [MinLength(10, ErrorMessage = "Description should be between 10 and 255 symbols.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Suggested price")]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name = "New price")]
        [Range(1, 10000000000, ErrorMessage = "You cannot sell it for less than a dollar.")]
        public decimal? OwnerCustomPrice { get; set; }
    }
}