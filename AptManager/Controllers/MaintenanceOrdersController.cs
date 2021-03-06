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
    public class MaintenanceOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaintenanceOrders
        public ActionResult Index()
        {
            var maintenanceOrders = db.MaintenanceOrders.Include(m => m.HousingUnit);
            return View(maintenanceOrders.ToList());
        }

        public ActionResult IndexMyWorkOrder()
        {

            if (User.Identity.IsAuthenticated && User.IsInRole("Maintenance" ))
            {
                var user = User.Identity.GetUserId();

                //b4bc8ccd - f6cd - 4346 - 8d69 - 83f33dcd7769
                        var worker = db.Workers.Where(w => w.ApplicationUserId == user).FirstOrDefault();
                var maintenanceOrders = db.MaintenanceOrders.Select(m => m.WorkerId == worker.WorkerId && m.IsCompleted != true).ToList();
                return View(maintenanceOrders.ToList());
            }
            return View();
        }

        // GET: MaintenanceOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceOrder maintenanceOrder = db.MaintenanceOrders.Find(id);
            if (maintenanceOrder == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceOrder);
        }

        // GET: MaintenanceOrders/TenantSubmission
        public ActionResult TenantWorkOrderSubmission()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Tenant"))
            {
                var id = User.Identity.GetUserId();
                var Tenant = db.Tenants.Where(t => t.TenantId.Equals(id)).First();
                ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId");
                return View(Tenant);
            }
            return View();
        }

        // POST: MaintenanceOrders/TenantSubmission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TenantWorkOrderSubmission([Bind(Include = "OrderId,UnitId,Name,Description,DueDate")] MaintenanceOrder maintenanceOrder, Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                //ADD TWILLIO API FUNCTIONALITY HERE??
                maintenanceOrder.HousingUnit.UnitId = tenant.HousingUnit.UnitId;
                db.MaintenanceOrders.Add(maintenanceOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
        }

        // GET: MaintenanceOrders/TenantSubmission
        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId");
            return View();
        }

        // POST: MaintenanceOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,UnitId,Name,Description,DueDate")] MaintenanceOrder maintenanceOrder)
        {
            if (ModelState.IsValid)
            {
                db.MaintenanceOrders.Add(maintenanceOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
        }

        // GET: MaintenanceOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceOrder maintenanceOrder = db.MaintenanceOrders.Find(id);
            if (maintenanceOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
        }

        // POST: MaintenanceOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UnitId,Name,Description,DueDate")] MaintenanceOrder maintenanceOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
        }

        // GET: MaintenanceOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceOrder maintenanceOrder = db.MaintenanceOrders.Find(id);
            if (maintenanceOrder == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceOrder);
        }

        // POST: MaintenanceOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceOrder maintenanceOrder = db.MaintenanceOrders.Find(id);
            db.MaintenanceOrders.Remove(maintenanceOrder);
            db.SaveChanges();
            return RedirectToAction("WorkOrders", "Manager" );
        }

        public ActionResult ConfirmComplete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MaintenanceOrder maintenanceOrder = db.MaintenanceOrders.Find(id);
            if (maintenanceOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
        }

        // POST: MaintenanceOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmComplete([Bind(Include = "OrderId,UnitId,Name,Description,DueDate,IsComplete")] MaintenanceOrder maintenanceOrder)
        {
            if (ModelState.IsValid)
            {
                if (maintenanceOrder.IsCompleted != true)
                {
                db.Entry(maintenanceOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexMyWorkOrder");

                }
            }
            ViewBag.UnitId = new SelectList(db.HousingUnits, "UnitId", "UnitId", maintenanceOrder.UnitId);
            return View(maintenanceOrder);
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
