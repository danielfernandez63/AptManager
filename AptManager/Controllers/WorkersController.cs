﻿using System;
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
    public class WorkersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Workers

        public ActionResult WorkerPortal()
        {
            return View();
        }

        public ActionResult WorkOrderList()
        {
            var currentWorker = User.Identity.GetUserId();
            Worker me = db.Workers.Where(m => m.ApplicationUserId == currentWorker).Single();
            var myWorkOrders = db.MaintenanceOrders.Where(w => w.WorkerId == me.WorkerId);
            return View(db.MaintenanceOrders.ToList());
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
        public ActionResult Create([Bind(Include = "UnitId,MonthlyRent,Bedrooms,SquareFootage,OutdoorAccess")] HousingUnit housingUnit)
        {
            if (ModelState.IsValid)
            {
                db.HousingUnits.Add(housingUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(housingUnit);
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
                return RedirectToAction("WorkerPortal");
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
            return RedirectToAction("WorkerPortal");
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