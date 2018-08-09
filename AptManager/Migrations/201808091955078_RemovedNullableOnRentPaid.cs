namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNullableOnRentPaid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tenants", "RentPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tenants", "RentPaid", c => c.Boolean());
        }
    }
}
