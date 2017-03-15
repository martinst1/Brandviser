namespace Brandviser.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BrandviserDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Brandviser.Data.BrandviserDbContext";
        }

        protected override void Seed(BrandviserDbContext context)
        {
            context.Statuses.AddOrUpdate(s => s.Name,
                new Status() { Id = 1, Name = "Pending" },
                new Status() { Id = 2, Name = "Rejected" },
                new Status() { Id = 3, Name = "Accepted" },
                new Status() { Id = 4, Name = "Published" },
                new Status() { Id = 5, Name = "Sold" }
                );
        }
    }
}