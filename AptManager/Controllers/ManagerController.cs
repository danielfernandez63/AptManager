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
                return View(visitor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VisitorsToTenants(Visitor visitor, Tenant tenant)
        {
            var user = (from v in db.Visitors where v.ApplicationUserId == visitor.ApplicationUserId select v).FirstOrDefault();
            Tenant newTenant = new Tenant();
            newTenant.ApplicationUserId = user.ApplicationUserId;
            newTenant.FirstName = user.FirstName;
            newTenant.LastName = user.LastName;
            newTenant.PhoneNumber = user.PhoneNumber;
            newTenant.Email = user.Email;
            db.Tenants.Add(newTenant);
            db.Visitors.Remove(user);
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
        public ActionResult Edit(int? id)
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

        // POST: HousingUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitId,MonthlyRent,Bedrooms,SquareFootage,OutdoorAccess")] HousingUnit housingUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(housingUnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(housingUnit);
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
