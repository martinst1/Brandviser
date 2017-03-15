using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Brandviser.Data.Models.Constants;

namespace Brandviser.Data.Models
{
    public class Domain
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User OriginalOwner { get; set; }

        public int? BuyerId { get; set; }

        public virtual User Buyer { get; set; }

        public int? DesignerId { get; set; }

        public virtual User Designer { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ModelConstants.DomainMinLength)]
        [MaxLength(ModelConstants.DomainMaxLength)]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public decimal? OriginalOwnerCustomPrice { get; set; }

        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        [Required]
        [MinLength(ModelConstants.DescriptionMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime? SoldOn { get; set; }

        public Guid VerificationId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string LogoUrl { get; set; }

        public Domain()
        {

        }

        public Domain(int userId, string name, int statusId, string description, DateTime createdAt)
            : this()
        {
            this.UserId = userId;
            this.Name = name;
            this.StatusId = statusId;
            this.Description = description;
            this.CreatedAt = createdAt;

            this.VerificationId = Guid.NewGuid();
        }
    }
}
