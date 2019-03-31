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
        public ActionResult Index()
        {
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
            var response = new TownCreateResponse
            {
                UserType = Common.GetUserType(User.Identity.Name)
            };

            return View(response);
        }

        // POST: Towns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TownId,Name")] Town town)
        {
            if (ModelState.IsValid)
            {
                var user = Common.GetUserByEmail(User.Identity.Name);

                town.CountryId = user.CountryId;

                if(user.UserType == Enums.UserType.Administrador)
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

                return RedirectToAction("Index");
            }

            return View(town);
        }

        // GET: Towns/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Towns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TownId,Name")] Town town)
        {
            if (ModelState.IsValid)
            {
                db.Entry(town).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(town);
        }

        // GET: Towns/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            id = Convert.ToInt32(Request["townId"]);

            Town town = db.Towns.Find(id);
            db.Towns.Remove(town);
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
