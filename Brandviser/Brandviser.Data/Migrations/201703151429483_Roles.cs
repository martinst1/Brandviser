namespace Brandviser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Roles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (1, 'Admin')");
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (2, 'Seller')");
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (3, 'Buyer')");
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (4, 'Designer')");
        }

        public override void Down()
        {
        }
    }
}
