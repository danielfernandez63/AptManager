namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDisplayNameAndNullibleProp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tenants", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.Tenants", new[] { "UnitId" });
            AlterColumn("dbo.Tenants", "UnitId", c => c.Int());
            CreateIndex("dbo.Tenants", "UnitId");
            AddForeignKey("dbo.Tenants", "UnitId", "dbo.HousingUnits", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tenants", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.Tenants", new[] { "UnitId" });
            AlterColumn("dbo.Tenants", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tenants", "UnitId");
            AddForeignKey("dbo.Tenants", "UnitId", "dbo.HousingUnits", "UnitId", cascadeDelete: true);
        }
    }
}
