using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class MonthlyBalancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MonthlyBalances
        public ActionResult Index()
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                return View(db.MonthlyBalances.ToList());
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: MonthlyBalances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBalance monthlyBalance = db.MonthlyBalances.Find(id);
            if (monthlyBalance == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBalance);
        }

        // GET: MonthlyBalances/Create
        public ActionResult Create()
        {
            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: MonthlyBalances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonthlyBalanceId,Month,Year,ApplicationUserId")] MonthlyBalance monthlyBalance)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (ModelState.IsValid)
                {
                    db.MonthlyBalances.Add(monthlyBalance);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(monthlyBalance);
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: MonthlyBalances/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MonthlyBalance monthlyBalance = db.MonthlyBalances.Find(id);
                if (monthlyBalance == null)
                {
                    return HttpNotFound();
                }

                return View(monthlyBalance);
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // POST: MonthlyBalances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MonthlyBalanceId,Month,Year,ApplicationUserId")] MonthlyBalance monthlyBalance)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(monthlyBalance).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(monthlyBalance);
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: MonthlyBalances/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MonthlyBalance monthlyBalance = db.MonthlyBalances.Find(id);
                if (monthlyBalance == null)
                {
                    return HttpNotFound();
                }

                return View(monthlyBalance);
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // POST: MonthlyBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                MonthlyBalance monthlyBalance = db.MonthlyBalances.Find(id);
                db.MonthlyBalances.Remove(monthlyBalance);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = DateTime.Now,
                    UserEmail = User.Identity.Name
                });

                ViewBag.Error = "Error inesperado, intente de nuevo!";

                return View();
            }
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
