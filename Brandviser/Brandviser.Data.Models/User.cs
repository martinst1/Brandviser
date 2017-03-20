using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Brandviser.Data.Models
{
    public class User : IdentityUser
    {
        private ICollection<Domain> sellerDomains;
        private ICollection<Domain> buyerDomains;
        private ICollection<Domain> designerDomains;

        public User()
        {
            this.sellerDomains = new HashSet<Domain>();
            this.buyerDomains = new HashSet<Domain>();
            this.designerDomains = new HashSet<Domain>();
        }

        public decimal Balance { get; set; }

        [Required]
        [MinLength(ModelConstants.FirstNameMinLength)]
        [MaxLength(ModelConstants.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelConstants.LastNameMinLength)]
        [MaxLength(ModelConstants.LastNameMaxLength)]
        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Domain> SellerDomains
        {
            get
            {
                return this.sellerDomains;
            }
            set
            {
                this.sellerDomains = value;
            }
        }

        public virtual ICollection<Domain> BuyerDomains
        {
            get
            {
                return this.buyerDomains;
            }
            set
            {
                this.buyerDomains = value;
            }
        }

        public virtual ICollection<Domain> DesignerDomains
        {
            get
            {
                return this.designerDomains;
            }
            set
            {
                this.designerDomains = value;
            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
