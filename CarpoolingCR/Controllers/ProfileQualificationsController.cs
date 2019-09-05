using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class ProfileQualificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProfileQualifications
        public ActionResult Index(string userId)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var profileQualification = db.ProfileQualifications.Where(x => x.Userid == userId)
                    .Include(x => x.User)
                    .Include(x => x.Qualifications)
                    .SingleOrDefault();

                return View(profileQualification);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: ProfileQualifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileQualification profileQualification = db.ProfileQualifications.Find(id);
            if (profileQualification == null)
            {
                return HttpNotFound();
            }
            return View(profileQualification);
        }

        // GET: ProfileQualifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileQualifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfileQualificationId,Userid")] ProfileQualification profileQualification)
        {
            if (ModelState.IsValid)
            {
                db.ProfileQualifications.Add(profileQualification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profileQualification);
        }

        // GET: ProfileQualifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileQualification profileQualification = db.ProfileQualifications.Find(id);
            if (profileQualification == null)
            {
                return HttpNotFound();
            }
            return View(profileQualification);
        }

        // POST: ProfileQualifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfileQualificationId,Userid")] ProfileQualification profileQualification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileQualification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileQualification);
        }

        // GET: ProfileQualifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileQualification profileQualification = db.ProfileQualifications.Find(id);
            if (profileQualification == null)
            {
                return HttpNotFound();
            }
            return View(profileQualification);
        }

        // POST: ProfileQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileQualification profileQualification = db.ProfileQualifications.Find(id);
            db.ProfileQualifications.Remove(profileQualification);
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
