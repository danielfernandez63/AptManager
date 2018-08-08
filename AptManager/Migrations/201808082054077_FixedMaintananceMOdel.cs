namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedMaintananceMOdel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaintenanceOrders", "IsCompleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaintenanceOrders", "IsCompleted", c => c.Boolean(nullable: false));
        }
    }
}
