namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seededmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.Managers", new[] { "UnitId" });
            AlterColumn("dbo.Managers", "UnitId", c => c.Int());
            CreateIndex("dbo.Managers", "UnitId");
            AddForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits");
            DropIndex("dbo.Managers", new[] { "UnitId" });
            AlterColumn("dbo.Managers", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.Managers", "UnitId");
            AddForeignKey("dbo.Managers", "UnitId", "dbo.HousingUnits", "UnitId", cascadeDelete: true);
        }
    }
}
