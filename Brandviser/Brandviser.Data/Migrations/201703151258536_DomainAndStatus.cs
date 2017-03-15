namespace Brandviser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DomainAndStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Domains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BuyerId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 259),
                        Price = c.Double(),
                        OriginalOwnerCustomPrice = c.Double(),
                        StatusId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 255),
                        SoldOn = c.DateTime(),
                        VerificationId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        LogoUrl = c.String(),
                        Buyer_Id = c.String(maxLength: 128),
                        OriginalOwner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Buyer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OriginalOwner_Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.Buyer_Id)
                .Index(t => t.OriginalOwner_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Domains", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Domains", "OriginalOwner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Domains", "Buyer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Domains", new[] { "OriginalOwner_Id" });
            DropIndex("dbo.Domains", new[] { "Buyer_Id" });
            DropIndex("dbo.Domains", new[] { "StatusId" });
            DropTable("dbo.Status");
            DropTable("dbo.Domains");
        }
    }
}
