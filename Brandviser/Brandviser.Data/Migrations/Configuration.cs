namespace Brandviser.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Brandviser.Data.BrandviserDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Brandviser.Data.BrandviserDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

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
