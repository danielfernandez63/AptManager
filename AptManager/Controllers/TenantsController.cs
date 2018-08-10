using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptManager.Models;
using Microsoft.AspNet.Identity;
using Stripe;

namespace AptManager.Controllers
{
    public class TenantsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Tenants
        public ActionResult Index()
        {


            var user = User.Identity.GetUserId();

            var loggedInUser = db.Tenants.Where(c => c.ApplicationUserId == user).Single();
           
           
            return View();
        }

        // GET: HousingUnits/Details/5
        public ActionResult Details(int? id)
        {
            var user = User.Identity.GetUserId();

            var loggedInUser = db.Tenants.Where(c => c.ApplicationUserId == user).Single();


            if (loggedInUser.ApplicationUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(loggedInUser);
         
        }

        // GET: HousingUnits/Details/5
        public ActionResult TicketComplete(int? id)
        {
            var user = User.Identity.GetUserId();

            var loggedInUser = db.Tenants.Where(c => c.ApplicationUserId == user).Single();


            if (loggedInUser.ApplicationUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(loggedInUser);

        }
        public ActionResult WorkOrderMessage(string phoneNumber, int UnitId)
        {
            string messageText = $"A maintenance report has been submitted for unit number: {UnitId}";
            TwilioNotification.TwilioMessage(phoneNumber, messageText);
            return View();
        }


        // GET: HousingUnits/Details/5
        public ActionResult PayRent(int? id)
        {
            var user = User.Identity.GetUserId();

            var loggedInUser = db.Tenants.Where(c => c.ApplicationUserId == user).Single();

            //Tenant tenant = te
            if (loggedInUser.ApplicationUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(loggedInUser);

        }

        

        public ActionResult CheckIfRentDueDate(int? id)
        {
            var tenant = db.Tenants.Find(id);
            int dueDate = 5; //TEMP!! NEED TO GRAB THIS FROM TENANT.DUEDATE
            int reminderDate = 1;
            if (DateTime.Now.Day == reminderDate)
            {
                string message = $"Dear, {tenant.FirstName} {tenant.LastName} your rent payment of {tenant.HousingUnit.MonthlyRent} is due on the 5th. Thank you";
                TwilioNotification.TwilioMessage(message, tenant.PhoneNumber);
            }

            else if (dueDate == DateTime.Now.Day)
            {
                ChargeRentBalance(tenant);
            }
            
            return RedirectToAction("");

        }

        public void ChargeRentBalance(Tenant tenant)
        {
            var unit = db.HousingUnits.Find(tenant.HousingUnit.UnitId);
            Tenant updatedTenant = db.Tenants.Find(tenant.TenantId);
            updatedTenant.BalanceDue = tenant.BalanceDue + unit.MonthlyRent;
            db.Entry(updatedTenant).State = EntityState.Modified;
            db.SaveChanges();
        }
        

        public ActionResult ManagerNotification(int? id)
        {
            var user = db.Tenants.Find(id);

            if (user.ApplicationUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("WorkOrderMessage", user);
        }
        // GET: HousingUnits/Create
        public ActionResult Create()

        {
            return View();
        }

        // POST: HousingUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,PhoneNumber,Email")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                tenant.ApplicationUserId = User.Identity.GetUserId();
                db.Tenants.Add(tenant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tenant);
        }

        // GET: MaintenanceOrders/TenantSubmission
        public ActionResult FileReport()
        {
            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId");
            return View();
        }

        // POST: MaintenanceOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileReport([Bind(Include = "OrderId,UnitId,Name,Description,DueDate")] MaintenanceOrder maintenanceOrder)
        {
            if (ModelState.IsValid)
            {
                db.MaintenanceOrders.Add(maintenanceOrder);
                db.SaveChanges();
                var manangerNumber = TwilioNotification.GetManagerNumber();
                WorkOrderMessage(manangerNumber, maintenanceOrder.UnitId);
                return RedirectToAction("TicketComplete");
            }

            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
        }

        // GET: HousingUnits/Edit/5
        public ActionResult Edit(int? id)
        {
  

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // POST: HousingUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "UnitId,MonthlyRent,Bedrooms,SquareFootage,OutdoorAccess")]*/ Tenant tenant)
        {

            var user = User.Identity.GetUserId();

            var loggedInUser = db.Tenants.Where(c => c.ApplicationUserId == user).Single();

            if (ModelState.IsValid)
            {
                loggedInUser.FirstName = tenant.FirstName;
                loggedInUser.LastName = tenant.LastName;
                loggedInUser.Email = tenant.Email;
                loggedInUser.PhoneNumber = tenant.PhoneNumber;

                db.Entry(loggedInUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(tenant);
        }

        // GET: HousingUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HousingUnit housingUnit = db.HousingUnits.Find(id);
            if (housingUnit == null)
            {
                return HttpNotFound();
            }
            return View(housingUnit);
        }

        // POST: HousingUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HousingUnit housingUnit = db.HousingUnits.Find(id);
            db.HousingUnits.Remove(housingUnit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Charge()
        {
            var stripePubKey = APIKeys.GetPubKey();


            var stripePublishKey = ConfigurationManager.AppSettings["pk_test_kKzDFMzgjOeAm8je9paxecf3"];


            ViewBag.StripePublishKey = stripePublishKey;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Charge(string stripeEmail, string stripeToken /*Tenant tenant*/)
        {
            var user = User.Identity.GetUserId();
            var loggedInUser = db.Tenants.Where(c => c.ApplicationUserId == user).Single();

            var cost = loggedInUser.BalanceDue;
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount =  cost,//charge in cents
                Description = "Rent Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
