namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedmodelerror : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HousingUnits",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        MonthlyRent = c.Int(nullable: false),
                        Bedrooms = c.Int(nullable: false),
                        SquareFootage = c.Int(nullable: false),
                        OutdoorAccess = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.MaintenanceOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UnitId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.HousingUnits", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        TenantId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        UnitId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        BalanceDue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TenantId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.HousingUnits", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        VisitorId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.VisitorId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        WorkerId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Visitors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tenants", "UnitId", "dbo.HousingUnits");
            DropForeignKey("dbo.Tenants", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Managers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MaintenanceOrders", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.Workers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Visitors", new[] { "ApplicationUserId" });
            DropIndex("dbo.Tenants", new[] { "UnitId" });
            DropIndex("dbo.Tenants", new[] { "ApplicationUserId" });
            DropIndex("dbo.Managers", new[] { "ApplicationUserId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "UnitId" });
            DropTable("dbo.Workers");
            DropTable("dbo.Visitors");
            DropTable("dbo.Tenants");
            DropTable("dbo.Managers");
            DropTable("dbo.MaintenanceOrders");
            DropTable("dbo.HousingUnits");
        }
    }
}
