namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reInitMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.MaintenanceOrders", new[] { "WorkerId" });
            DropIndex("dbo.Managers", new[] { "UnitId" });
            AddColumn("dbo.MaintenanceOrders", "IsCompleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MaintenanceOrders", "WorkerId", c => c.Int());
            AlterColumn("dbo.MaintenanceOrders", "DueDate", c => c.DateTime());
            AlterColumn("dbo.Managers", "UnitId", c => c.Int());
            CreateIndex("dbo.MaintenanceOrders", "WorkerId");
            CreateIndex("dbo.Managers", "UnitId");
            AddForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers", "WorkerId");
            AddForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits");
            DropForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Managers", new[] { "UnitId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "WorkerId" });
            AlterColumn("dbo.Managers", "UnitId", c => c.Int(nullable: false));
            AlterColumn("dbo.MaintenanceOrders", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MaintenanceOrders", "WorkerId", c => c.Int(nullable: false));
            DropColumn("dbo.MaintenanceOrders", "IsCompleted");
            CreateIndex("dbo.Managers", "UnitId");
            CreateIndex("dbo.MaintenanceOrders", "WorkerId");
            AddForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits", "UnitId", cascadeDelete: true);
            AddForeignKey("dbo.MaintenanceOrders", "WorkerId", "dbo.Workers", "WorkerId", cascadeDelete: true);
        }
    }
}
