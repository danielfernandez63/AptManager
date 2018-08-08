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
