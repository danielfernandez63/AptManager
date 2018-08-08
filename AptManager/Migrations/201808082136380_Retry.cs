namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Retry : DbMigration
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
                        WorkerId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        DueDate = c.DateTime(),
                        IsCompleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.HousingUnits", t => t.UnitId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId)
                .Index(t => t.UnitId)
                .Index(t => t.WorkerId);
            
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        UnitId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.HousingUnits", t => t.UnitId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        TenantId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        UnitId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        BalanceDue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TenantId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.HousingUnits", t => t.UnitId)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tenants", "UnitId", "dbo.HousingUnits");
            DropForeignKey("dbo.Tenants", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits");
            DropForeignKey("dbo.Managers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Workers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MaintenanceOrders", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.Visitors", new[] { "ApplicationUserId" });
            DropIndex("dbo.Tenants", new[] { "UnitId" });
            DropIndex("dbo.Tenants", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Managers", new[] { "UnitId" });
            DropIndex("dbo.Managers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Workers", new[] { "ApplicationUserId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "WorkerId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "UnitId" });
            DropTable("dbo.Visitors");
            DropTable("dbo.Tenants");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Managers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Workers");
            DropTable("dbo.MaintenanceOrders");
            DropTable("dbo.HousingUnits");
        }
    }
}
