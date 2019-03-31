using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarpoolingCR.Models;
using CarpoolingCR.Utils;

namespace CarpoolingCR.Controllers
{
    public class RoutesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Routes
        public ActionResult Index()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            //var routes = db.Routes.Include(r => r.Journey);
            return View();
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RouteId,JourneyId,Name,Status")] Route route)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", route.JourneyId);
            return View(route);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", route.JourneyId);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RouteId,JourneyId,Name,Status")] Route route)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.JourneyId = new SelectList(db.Journeys, "JourneyId", "Name", route.JourneyId);
            return View(route);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
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
