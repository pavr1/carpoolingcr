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
        public ActionResult Index(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Info = message;
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                TownIndexResponse response = new TownIndexResponse
                {
                    UserType = user.UserType
                };

                if (user.UserType == Enums.UserType.Administrador)
                {
                    response.Towns = db.Towns
                        .Include(x => x.Country)
                        .ToList();
                }
                else
                {
                    response.Towns = db.Towns.Where(x => x.CountryId == user.CountryId && x.Status == Enums.TownStatus.Active)
                       .Include(x => x.Country)
                       .ToList();
                }

                return View(response);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        // GET: Towns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Town town = db.Towns.Find(id);
            if (town == null)
            {
                return HttpNotFound();
            }
            return View(town);
        }

        // GET: Towns/Create
        public ActionResult Create()
        {
            try
            {
                var response = new TownCreateResponse
                {
                    UserType = Common.GetUserType(User.Identity.Name)
                };

                return View(response);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        // POST: Towns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TownId,Name")] Town town)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = Common.GetUserByEmail(User.Identity.Name);

                    town.CountryId = user.CountryId;

                    if (user.UserType == Enums.UserType.Administrador)
                    {
                        town.Status = Enums.TownStatus.Active;
                    }
                    else
                    {
                        town.Status = Enums.TownStatus.Pending;
                    }

                    db.Towns.Add(town);
                    db.SaveChanges();

                    EmailHandler.SendEmailNewTown();

                    return RedirectToAction("Index", new { message = "Localidad creada satisfactoriamente pero no será visible hasta la aprobación del administrador."});
                }

                return View(town);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        // GET: Towns/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Town town = db.Towns.Find(id);
                if (town == null)
                {
                    return HttpNotFound();
                }
                return View(town);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        // POST: Towns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TownId,CountryId,Status,Name")] Town town)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(town).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { message = "La localidad ha sido actualizada!" });
                }

                return View(town);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        // GET: Towns/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Town town = db.Towns.Find(id);
                if (town == null)
                {
                    return HttpNotFound();
                }
                return View(town);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

                return View();
            }
        }

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                id = Convert.ToInt32(Request["townId"]);

                Town town = db.Towns.Find(id);
                db.Towns.Remove(town);
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

                ViewBag.Error = "Hubo un error inesperado, por favor intente de nuevo.";

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
