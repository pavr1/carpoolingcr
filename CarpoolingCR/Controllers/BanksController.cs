using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class BanksController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Banks
        //public ActionResult Index(string message)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (!string.IsNullOrEmpty(message))
        //        {
        //            ViewBag.Info = message;
        //        }

        //        return View(db.Banks.ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: Banks/Details/5
        //public ActionResult Details(int? id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Bank bank = db.Banks.Find(id);
        //        if (bank == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(bank);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: Banks/Create
        //public ActionResult Create()
        //{
        //    if (!Common.IsAuthorized(User))
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    return View();
        //}

        //// POST: Banks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "BankId,BankName")] Bank bank)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            db.Banks.Add(bank);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        return View(bank);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: Banks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Bank bank = db.Banks.Find(id);
        //        if (bank == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(bank);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// POST: Banks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "BankId,BankName")] Bank bank)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(bank).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        return View(bank);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// GET: Banks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Bank bank = db.Banks.Find(id);
        //        if (bank == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(bank);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //// POST: Banks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        if (!Common.IsAuthorized(User))
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        id = Convert.ToInt32(Request["bankId"]);

        //        Bank bank = db.Banks.Find(id);
        //        db.Banks.Remove(bank);
        //        db.SaveChanges();

        //        //¡Banco Eliminado!
        //        return RedirectToAction("Index", new { message = "10008"});
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogData(new Log
        //        {
        //            Line = Common.GetCurrentLine(),
        //            Location = Enums.LogLocation.Server,
        //            LogType = Enums.LogType.Error,
        //            Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
        //            Method = Common.GetCurrentMethod(),
        //            Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
        //            UserEmail = User.Identity.Name
        //        });

        //        ViewBag.Error = "¡Error inesperado, intente de nuevo!";

        //        return View();
        //    }
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
