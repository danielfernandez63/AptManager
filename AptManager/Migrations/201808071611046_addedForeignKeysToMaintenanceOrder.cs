namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedForeignKeysToMaintenanceOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaintenanceOrders", "WorkerId", c => c.Int(nullable: false));
            AddColumn("dbo.Managers", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.MaintenanceOrders", "WorkerId");
            CreateIndex("dbo.Managers", "UnitId");
            AddForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers", "WorkerId", cascadeDelete: true);
            AddForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits", "UnitId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits");
            DropForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Managers", new[] { "UnitId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "WorkerId" });
            DropColumn("dbo.Managers", "UnitId");
            DropColumn("dbo.MaintenanceOrders", "WorkerId");
        }
    }
}
