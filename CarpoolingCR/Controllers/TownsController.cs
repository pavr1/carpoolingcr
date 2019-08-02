using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class TownsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Towns
        public ActionResult Index(string message, string type)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    if (!string.IsNullOrEmpty(message))
            //    {
            //        if (type == "info")
            //        {
            //            ViewBag.Info = message;
            //        }
            //        else if (type == "error")
            //        {
            //            ViewBag.Error = message;
            //        }
            //        else if (type == "warining")
            //        {
            //            ViewBag.Warning = message;
            //        }
            //    }

            //    var user = Common.GetUserByEmail(User.Identity.Name);

            //    TownIndexResponse response = new TownIndexResponse
            //    {
            //        UserType = user.UserType
            //    };

            //    if (user.UserType == Enums.UserType.Administrador)
            //    {
            //        response.Towns = db.Towns
            //            .Include(x => x.Country)
            //            .OrderBy(x => x.Name)
            //            .ToList();
            //    }
            //    else
            //    {
            //        response.Towns = db.Towns.Where(x => x.CountryId == user.CountryId && x.Status == Enums.TownStatus.Active)
            //           .Include(x => x.Country)
            //           .OrderBy(x => x.Name)
            //           .ToList();
            //    }

            //    return View(response);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}

            return View();
        }

        // GET: Towns/Details/5
        public ActionResult Details(int? id)
        {
            //if (!Common.IsAuthorized(User))
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Town town = db.Towns.Find(id);
            //if (town == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(town);
            return View();
        }

        // GET: Towns/Create
        public ActionResult Create()
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    var response = new TownCreateResponse
            //    {
            //        UserType = Common.GetUserType(User.Identity.Name)
            //    };

            //    return View(response);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
        }

        // POST: Towns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TownId,Name")] int town)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        var existentTown = db.Towns.Where(x => x.Name.ToUpper() == town.Name.ToUpper()).SingleOrDefault();

            //        if (existentTown != null)
            //        {
            //            //¡La localidad ya existe!
            //            ViewBag.Error = "10004";

            //            return View(new TownCreateResponse
            //            {
            //                UserType = Common.GetUserType(User.Identity.Name),
            //                Town = town
            //            });
            //        }

            //        var user = Common.GetUserByEmail(User.Identity.Name);

            //        town.CountryId = user.CountryId;

            //        if (user.UserType == Enums.UserType.Administrador)
            //        {
            //            town.Status = Enums.TownStatus.Active;
            //        }
            //        else
            //        {
            //            town.Status = Enums.TownStatus.Pending;
            //        }

            //        db.Towns.Add(town);
            //        db.SaveChanges();

            //        var callbackUrl = Url.Action("Index", "Towns", new { }, protocol: Request.Url.Scheme);

            //        EmailHandler.SendEmailNewTown(callbackUrl);

            //        //¡Destino/Origen Creado!
            //        return RedirectToAction("Index", new { message = "100019", type = "info" });
            //    }

            //    return View(town);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
        }

        // GET: Towns/Edit/5
        public ActionResult Edit(int? id)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Town town = db.Towns.Find(id);
            //    if (town == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(town);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
        }

        // POST: Towns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TownId,CountryId,Status,Name")] int town)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(town).State = EntityState.Modified;
            //        db.SaveChanges();

            //        //¡Destino/Origen Actualizado!
            //        return RedirectToAction("Index", new { message = "100020", type = "info" });
            //    }

            //    return View(town);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
        }

        // GET: Towns/Delete/5
        public ActionResult Delete(int? id)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Town town = db.Towns.Find(id);
            //    if (town == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(town);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
        }

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //try
            //{
            //    if (!Common.IsAuthorized(User))
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    id = Convert.ToInt32(Request["townId"]);

            //    Town town = db.Towns.Find(id);
            //    db.Towns.Remove(town);
            //    db.SaveChanges();

            //    //¡Origen/Destino Eliminado!
            //    return RedirectToAction("Index", new { message = "100021", type = "info" });
            //}
            //catch (Exception ex)
            //{
            //    Common.LogData(new Log
            //    {
            //        Line = Common.GetCurrentLine(),
            //        Location = Enums.LogLocation.Server,
            //        LogType = Enums.LogType.Error,
            //        Message = ex.Message + " / " + ex.StackTrace,
            //        Method = Common.GetCurrentMethod(),
            //        Timestamp = Common.ConvertToUTCTime(DateTime.Now),
            //        UserEmail = User.Identity.Name
            //    });

            //    ViewBag.Error = "¡Error inesperado, intente de nuevo!";

            //    return View();
            //}
            return View();
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
