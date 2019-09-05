using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;

namespace CarpoolingCR.Controllers
{
    public class NotificationRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NotificationRequests
        public ActionResult Index()
        {
            var notificationRequests = db.NotificationRequests.Include(n => n.Reservation);
            return View(notificationRequests.ToList());
        }

        // GET: NotificationRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            if (notificationRequest == null)
            {
                return HttpNotFound();
            }
            return View(notificationRequest);
        }

        // GET: NotificationRequests/Create
        public ActionResult Create()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);

                var response = new NotificationsRequestResponse
                {
                    DistrictSelectOptionsHtml = districtsSelectHtml
                };

                //ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId");
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // POST: NotificationRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NotificationRequestId,UserId,FromTownId,ToTownId,CreatedDate,RequestedFromDateTime,RequestedToDateTime,ReservationId,Status")] NotificationRequest notificationRequest)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            var fields = "Fields => ";

            if (!Common.IsAuthorized(User))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = Common.GetUserByEmail(User.Identity.Name);

            try
            {
                #region Fields
                fields += "FromTown: " + Request["FromTown"];
                fields += "ToTown: " + Request["ToTown"] + ", ";
                fields += "Route: " + Request["Route"] + ", ";
                fields += "AvailableSpaces: " + Request["AvailableSpaces"];
                fields += "DateTime: " + Request["DateTime"];
                fields += "Trip.Details: " + Request["Trip.Details"];
                fields += "Trip.Price: " + Request["Trip.Price"];
                fields += "TotalSpaces: " + Request["TotalSpaces"];
                #endregion

                var districtsSelectHtml = Common.GetLocationsStrings(user.CountryId);
                var fromDistrict = new District();
                var toDistrict = new District();

                fromDistrict = Common.ValidateDistrictString(Request["FromTown"]);

                if (fromDistrict == null)
                {
                    //¡Origen no válido!
                    ViewBag.Warning = "10005";

                    var response = new NotificationsRequestResponse
                    {
                        DistrictSelectOptionsHtml = districtsSelectHtml
                    };

                    return View(response);
                }

                toDistrict = Common.ValidateDistrictString(Request["ToTown"]);

                if (toDistrict == null)
                {
                    //¡Destino no válido!
                    ViewBag.Warning = "10006";

                    var response = new NotificationsRequestResponse
                    {
                        DistrictSelectOptionsHtml = districtsSelectHtml
                    };

                    return View(response);
                }

                var tripDate = DateTime.SpecifyKind(Convert.ToDateTime(Request["DateTimeDisplay"]), DateTimeKind.Local);
                var timeFlexibleCheck = Convert.ToBoolean(Request["time-options-check"]);
                var timeDisplay = Request["TimeDisplay"];
                var hourType = Convert.ToInt32(Request["hour-type"]);

                var createdDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                var requestedFromDateTime = DateTime.SpecifyKind(tripDate, DateTimeKind.Local);
                var requestedToDateTime = DateTime.SpecifyKind(tripDate, DateTimeKind.Local);


                //get time by hour-type
                if (timeFlexibleCheck)
                {
                    switch (hourType)
                    {
                        case 1:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 0, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 23, 59, 0);
                            break;
                        case 2:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 0, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 5, 59, 0);
                            break;
                        case 3:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 6, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 11, 59, 0);
                            break;
                        case 4:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 12, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 12, 59, 0);
                            break;
                        case 5:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 13, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 18, 59, 0);
                            break;
                        case 6:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 19, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 23, 59, 0);
                            break;
                        default:
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 0, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 23, 59, 0);
                            break;
                    }
                }
                else
                {
                    //get user specific time

                    //split timeDisplay 5:15 PM to get hour and minutes, substract 15 mins before to get FromDateTime, and sum up 15 mins after to get toDateTime
                    DateTime userSelectedTime = DateTime.Parse("05:00 PM");

                    var fromUserSelectedTime = userSelectedTime.AddMinutes(-15);
                    var toUserSelectedTime = userSelectedTime.AddMinutes(15);

                    requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, fromUserSelectedTime.Hour, fromUserSelectedTime.Minute, 0);
                    requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, toUserSelectedTime.Hour, toUserSelectedTime.Minute, 0);
                }

                notificationRequest = new NotificationRequest
                {
                    CreatedDate = Common.ConvertToUTCTime(createdDate),
                    FromTownId = fromDistrict.DistrictId,
                    ToTownId = toDistrict.DistrictId,
                    RequestedFromDateTime = Common.ConvertToUTCTime(requestedFromDateTime),
                    RequestedToDateTime = Common.ConvertToUTCTime(requestedToDateTime),
                    Status = Enums.RequestNotificationStatus.Active,
                    UserId = user.Id
                };

                db.NotificationRequests.Add(notificationRequest);
                db.SaveChanges();

                return RedirectToAction("Index?message=Solicitud de notificacion creada");
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

        // GET: NotificationRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            if (notificationRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId", notificationRequest.ReservationId);
            return View(notificationRequest);
        }

        // POST: NotificationRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NotificationRequestId,UserId,FromTownId,ToTownId,CreatedDate,RequestedFromDateTime,RequestedToDateTime,ReservationId,Status")] NotificationRequest notificationRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificationRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "ReservationId", "ApplicationUserId", notificationRequest.ReservationId);
            return View(notificationRequest);
        }

        // GET: NotificationRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            if (notificationRequest == null)
            {
                return HttpNotFound();
            }
            return View(notificationRequest);
        }

        // POST: NotificationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotificationRequest notificationRequest = db.NotificationRequests.Find(id);
            db.NotificationRequests.Remove(notificationRequest);
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
