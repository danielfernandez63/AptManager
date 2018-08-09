namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRentPropertiesToTenantAndForeignKeyToWorker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "ManagerId", c => c.Int());
            AddColumn("dbo.Tenants", "RentDueDate", c => c.DateTime());
            AddColumn("dbo.Tenants", "RentPaid", c => c.Boolean());
            CreateIndex("dbo.Workers", "ManagerId");
            AddForeignKey("dbo.Workers", "ManagerId", "dbo.Managers", "ManagerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "ManagerId", "dbo.Managers");
            DropIndex("dbo.Workers", new[] { "ManagerId" });
            DropColumn("dbo.Tenants", "RentPaid");
            DropColumn("dbo.Tenants", "RentDueDate");
            DropColumn("dbo.Workers", "ManagerId");
        }
    }
}
