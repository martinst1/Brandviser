using System.Data.Entity;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Brandviser.Data
{
    public class BrandviserDbContext : IdentityDbContext<User>, IBrandviserDbContext
    {
        public BrandviserDbContext()
            : base("BrandviserDb", throwIfV1Schema: false)
        {
            //Database.SetInitializer<BrandviserDbContext>(new CreateDatabaseIfNotExists<BrandviserDbContext>());
        }

        public virtual IDbSet<Domain> Domains { get; set; }

        public virtual IDbSet<Status> Statuses { get; set; }

        public static BrandviserDbContext Create()
        {
            return new BrandviserDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                        .HasMany(x => x.SellerDomains)
                        .WithOptional(x => x.User)
                        .HasForeignKey(x => x.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany(x => x.BuyerDomains)
                        .WithOptional(x => x.Buyer)
                        .HasForeignKey(x => x.BuyerId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany<Domain>(x => x.DesignerDomains)
                        .WithOptional(x => x.Designer)
                        .HasForeignKey(x => x.DesignerId)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
