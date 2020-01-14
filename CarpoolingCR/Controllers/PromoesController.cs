using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarpoolingCR.Models;
using CarpoolingCR.Models.Promos;
using CarpoolingCR.Utils;

namespace CarpoolingCR.Controllers
{
    public class PromoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promoes
        public ActionResult Index()
        {
            var promo = db.Promo.Include(p => p.PromoType);

            var user = Common.GetUserByEmail(User.Identity.Name);
            Common.UpdateMenuItemsCount(user.Id);

            return View(promo.ToList());
        }

        public ActionResult IndexDetail()
        {
            return View();
        }

        // GET: Promoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promo.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // GET: Promoes/Create
        public ActionResult Create()
        {
            ViewBag.PromoTypeId = new SelectList(db.PromoType, "PromoTypeId", "Description");
            return View();
        }

        // POST: Promoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromoId,PromoTypeId,Amount,StartTime,EndTime,UntilAssignedAmountRunsOut,Status,MaxAmountPerUser,Description,MaxAmountToSpend,AmountAvailable")] Promo promo)
        {
            if (ModelState.IsValid)
            {
                if (promo.UntilAssignedAmountRunsOut)
                {
                    promo.EndTime = null;
                }
                else
                {
                    promo.MaxAmountToSpend = null;
                }

                promo.StartTime = Common.ConvertToUTCTime(promo.StartTime);

                if (promo.EndTime != null)
                {
                    promo.EndTime = Common.ConvertToUTCTime((DateTime)promo.EndTime);
                }

                promo.AmountAvailable = promo.MaxAmountToSpend;
                promo.Status = Utils.Enums.PromoStatus.Active;
                promo.MaxTimesPerUser = Convert.ToInt32(Request["MaxTimesPerUser"]);

                db.Promo.Add(promo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PromoTypeId = new SelectList(db.PromoType, "PromoTypeId", "Description", promo.PromoTypeId);
            return View(promo);
        }

        // GET: Promoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promo.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            ViewBag.PromoTypeId = new SelectList(db.PromoType, "PromoTypeId", "Description", promo.PromoTypeId);
            return View(promo);
        }

        // POST: Promoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromoId,PromoTypeId,Amount,StartTime,EndTime,UntilAssignedAmountRunsOut,Status,MaxAmountPerUser,Description,MaxAmountToSpend,AmountAvailable")] Promo promo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PromoTypeId = new SelectList(db.PromoType, "PromoTypeId", "Description", promo.PromoTypeId);
            return View(promo);
        }

        // GET: Promoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promo.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // POST: Promoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promo promo = db.Promo.Find(id);
            db.Promo.Remove(promo);
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
