namespace AptManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AptManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AptManager.Models.ApplicationDbContext context)
        {


            context.HousingUnits.AddOrUpdate(hse => hse.UnitId,
            new Models.HousingUnit() { UnitId = 1, Bedrooms = 2, MonthlyRent = 1775, OutdoorAccess = false, SquareFootage = 1050 });

            context.Managers.AddOrUpdate(mng => mng.ManagerId,
                new Models.Manager() { ManagerId = 1, FirstName = "Lisa", LastName = "Bock", Email = "Bock@gmail.com", PhoneNumber = "6086035693" , ApplicationUserId = "11778b5f-f710-4d3c-aca0-b6ca2b441152" });

            context.Workers.AddOrUpdate(wrk => wrk.WorkerId,
                new Models.Worker() { WorkerId = 1, FirstName = "Peter", LastName = "Griffin", Email = "Peter@gmail.com", PhoneNumber = "6905893697", ApplicationUserId = "ebd36e7b-85e4-4359-96ee-d76967f845bf"  });

            context.Tenants.AddOrUpdate(tnt => tnt.TenantId,
            new Models.Tenant() { TenantId = 1, FirstName = "Paxton", LastName = " Logan", BalanceDue = 0, Email = "Paxton@gmail.com", PhoneNumber = "6086908875", UnitId = 1, ApplicationUserId = "03da613e-5130-4c23-bea6-822fc636fd57" });


            context.MaintenanceOrders.AddOrUpdate(ord => ord.OrderId,
                new Models.MaintenanceOrder() { OrderId = 1, UnitId = 1, Name = "Broken sink", Description = "PLease fix sink in bathroom" });






            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}