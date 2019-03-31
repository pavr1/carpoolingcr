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
    public class MonthlyTransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MonthlyTransactions
        public ActionResult Index()
        {
            var monthlyTransactions = db.MonthlyTransactions.Include(m => m.MonthlyBalance);
            return View(monthlyTransactions.ToList());
        }

        // GET: MonthlyTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyTransactions monthlyTransactions = db.MonthlyTransactions.Find(id);
            if (monthlyTransactions == null)
            {
                return HttpNotFound();
            }
            return View(monthlyTransactions);
        }

        // GET: MonthlyTransactions/Create
        public ActionResult Create()
        {
            ViewBag.MonthlyBalanceId = new SelectList(db.MonthlyBalances, "MonthlyBalanceId", "MonthlyBalanceId");
            return View();
        }

        // POST: MonthlyTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonthlyTransactionsId,MonthlyBalanceId,InitialBalance,CreditAmount,CreditType,CreditReference,DebitAmount,DebitType,DebitReference,FinalBalance,Date")] MonthlyTransactions monthlyTransactions)
        {
            if (ModelState.IsValid)
            {
                db.MonthlyTransactions.Add(monthlyTransactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonthlyBalanceId = new SelectList(db.MonthlyBalances, "MonthlyBalanceId", "MonthlyBalanceId", monthlyTransactions.MonthlyBalanceId);
            return View(monthlyTransactions);
        }

        // GET: MonthlyTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyTransactions monthlyTransactions = db.MonthlyTransactions.Find(id);
            if (monthlyTransactions == null)
            {
                return HttpNotFound();
            }
            ViewBag.MonthlyBalanceId = new SelectList(db.MonthlyBalances, "MonthlyBalanceId", "MonthlyBalanceId", monthlyTransactions.MonthlyBalanceId);
            return View(monthlyTransactions);
        }

        // POST: MonthlyTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MonthlyTransactionsId,MonthlyBalanceId,InitialBalance,CreditAmount,CreditType,CreditReference,DebitAmount,DebitType,DebitReference,FinalBalance,Date")] MonthlyTransactions monthlyTransactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyTransactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonthlyBalanceId = new SelectList(db.MonthlyBalances, "MonthlyBalanceId", "MonthlyBalanceId", monthlyTransactions.MonthlyBalanceId);
            return View(monthlyTransactions);
        }

        // GET: MonthlyTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyTransactions monthlyTransactions = db.MonthlyTransactions.Find(id);
            if (monthlyTransactions == null)
            {
                return HttpNotFound();
            }
            return View(monthlyTransactions);
        }

        // POST: MonthlyTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyTransactions monthlyTransactions = db.MonthlyTransactions.Find(id);
            db.MonthlyTransactions.Remove(monthlyTransactions);
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
