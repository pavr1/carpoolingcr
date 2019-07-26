using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarpoolingCR;
using CarpoolingCR.Models;
using CarpoolingCR.Utils;

namespace CarpoolingCR.Controllers
{
    public class LogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Logs
        public ActionResult Index()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.Logs.ToList());
        }

        // GET: Logs/Details/5
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
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Logs/Create
        public ActionResult Create()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogId,Timestamp,LogType,Location,Message,Method,Line")] Log log)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(log);
        }

        // GET: Logs/Edit/5
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
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogId,Timestamp,LogType,Location,Message,Method,Line")] Log log)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(log);
        }

        // GET: Logs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
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
