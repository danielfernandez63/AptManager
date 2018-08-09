using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptManager.Models;
using Microsoft.AspNet.Identity;

namespace AptManager.Controllers
{
    public class ManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Manager

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visitors()
        {
            return View(db.Visitors.ToList());
        }

        public ActionResult Tenants()
        {
            return View(db.Tenants.ToList());
        }

        public ActionResult WorkOrders()
        {
            return View(db.MaintenanceOrders.ToList());
        }

        public ActionResult AssignWorkOrder(int? id, Worker worker)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MaintenanceOrder order = db.MaintenanceOrders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Workers = new SelectList(db.Workers.OrderBy(w => w.WorkerId).ToList(), "Id");
                return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignWorkOrder(Worker worker, MaintenanceOrder order)
        {
            var workOrder = (from w in db.MaintenanceOrders where w.OrderId == order.OrderId select w).SingleOrDefault();
            var assignedWorker = (from a in db.Workers where a.WorkerId == worker.WorkerId select a).SingleOrDefault();
            workOrder.WorkerId = assignedWorker.WorkerId;
            db.MaintenanceOrders.Add(workOrder);
            db.SaveChanges();
            return RedirectToAction("WorkOrders", "Manager");
        }

        public ActionResult VisitorsToTenants(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Visitor visitor = db.Visitors.Find(id);
                if (visitor == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Roles = new SelectList(db.Roles.Where(u => u.Name.Contains("Tenant")).ToList(), "Name", "Name");
                return View(visitor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VisitorsToTenants([Bind(Include = "TenantId,ApplicationUserId,FirstName,LastName,PhoneNumber,Email")] Visitor model, HousingUnit housingUnit)
        {

            //var user = (from v in db.Visitors where v.VisitorId == model.VisitorId select v).SingleOrDefault();
            Tenant newTenant = new Tenant();
            newTenant.ApplicationUserId = model.ApplicationUserId;
            newTenant.FirstName = model.FirstName;
            newTenant.LastName = model.LastName;
            newTenant.PhoneNumber = model.PhoneNumber;
            newTenant.Email = model.Email;
            db.Tenants.Add(newTenant);
            //db.Visitors.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Visitors", "Manager");
        }

        // GET: HousingUnits/Details/5
        public ActionResult Details(int? id)
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

        //VERY MVP, NEEDS A LOT MORE USER INPUT OPTIONS. WANT TO ADD PARTIAL VIEW
        public ActionResult LateRentMessage(string phoneNumber, int balance)
        {

            string messageText = $"Your past due rent balance is: {balance}. Please remit payment at your earliest convenience";
            TwilioNotification.TwilioMessage(phoneNumber, messageText);
            return View(); 

        }

        public ActionResult TenantNotification(int? id)
        {
            var user = db.Tenants.Find(id);

             if (user.ApplicationUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);

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
        public ActionResult Create([Bind(Include = "VisitorId,ApplicationUserId,FirstName,LastName,PhoneNumber,Email")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                visitor.ApplicationUserId = User.Identity.GetUserId();
                db.Visitors.Add(visitor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visitor);
        }

        // GET: HousingUnits/Edit/5
        public ActionResult EditTenant(int? id)
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
        public ActionResult EditTenant([Bind(Include = "TenantId,FirstName,LastName,UnitId,PhoneNumber,Email,BalanceDue,ApplicationUserId")] HousingUnit tenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Visitor visitor = db.Visitors.Find(id);
            if ( visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // POST: HousingUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitor visitor = db.Visitors.Find(id);
            db.Visitors.Remove(visitor);
            db.SaveChanges();
            return RedirectToAction("Visitors");
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
