using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var bankAccounts = db.BankAccounts.Include(b => b.Bank);
                return View(bankAccounts.ToList());
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

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName");
                return View();
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

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccountId,BankId,SavingsAccount,Sinpe")] BankAccount bankAccount)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (ModelState.IsValid)
                {
                    db.BankAccounts.Add(bankAccount);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName", bankAccount.BankId);
                return View(bankAccount);
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

                return View(bankAccount);
            }
        }

        // GET: BankAccounts/Edit/5
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
                BankAccount bankAccount = db.BankAccounts.Find(id);
                if (bankAccount == null)
                {
                    return HttpNotFound();
                }
                ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName", bankAccount.BankId);
                return View(bankAccount);
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

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankAccountId,BankId,SavingsAccount,Sinpe")] BankAccount bankAccount)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(bankAccount).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName", bankAccount.BankId);

                return View(bankAccount);
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

                return View(bankAccount);
            }
        }

        // GET: BankAccounts/Delete/5
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

                BankAccount bankAccount = db.BankAccounts.Find(id);
                if (bankAccount == null)
                {
                    return HttpNotFound();
                }

                return View(bankAccount);
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

        // POST: BankAccounts/Delete/5
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

                BankAccount bankAccount = db.BankAccounts.Find(id);
                db.BankAccounts.Remove(bankAccount);
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
