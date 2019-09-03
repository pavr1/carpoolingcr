using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarpoolingCR.Models;

namespace CarpoolingCR.Controllers
{
    public class NotificationRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NotificationRequests
        public ActionResult Index()
        {
            var notificationRequests = db.NotificationRequests.Include(n => n.Reservation);
            return View(notificationRequests.ToList());
        }

        // GET: NotificationRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            if (notificationRequest == null)
            {
                return HttpNotFound();
            }
            return View(notificationRequest);
        }

        // GET: NotificationRequests/Create
        public ActionResult Create()
        {
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId");
            return View();
        }

        // POST: NotificationRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NotificationRequestId,UserId,FromTownId,ToTownId,CreatedDate,RequestedFromDateTime,RequestedToDateTime,ReservationId,Status")] NotificationRequest notificationRequest)
        {
            if (ModelState.IsValid)
            {
                db.NotificationRequests.Add(notificationRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId", notificationRequest.ReservationId);
            return View(notificationRequest);
        }

        // GET: NotificationRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            if (notificationRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId", notificationRequest.ReservationId);
            return View(notificationRequest);
        }

        // POST: NotificationRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NotificationRequestId,UserId,FromTownId,ToTownId,CreatedDate,RequestedFromDateTime,RequestedToDateTime,ReservationId,Status")] NotificationRequest notificationRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificationRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId", notificationRequest.ReservationId);
            return View(notificationRequest);
        }

        // GET: NotificationRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            if (notificationRequest == null)
            {
                return HttpNotFound();
            }
            return View(notificationRequest);
        }

        // POST: NotificationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            db.NotificationRequests.Remove(notificationRequest);
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
