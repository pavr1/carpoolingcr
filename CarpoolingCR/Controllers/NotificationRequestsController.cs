using CarpoolingCR.Models;
using CarpoolingCR.Models.Locations;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class NotificationRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NotificationRequests
        public ActionResult Index(string message)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Info = message;
                }

                var notificationRequests = db.NotificationRequests
                    .Where(x => x.UserId == user.Id)
                    .Where(x => x.Status == Enums.RequestNotificationStatus.Active)
                    .Include(n => n.Reservation)
                    .ToList();

                foreach (var notification in notificationRequests)
                {
                    notification.RequestedFromDateTime = Common.ConvertToLocalTime(notification.RequestedFromDateTime);
                    notification.RequestedToDateTime = Common.ConvertToLocalTime(notification.RequestedToDateTime);

                    notification.FromTown = db.Districts.Where(x => x.DistrictId == notification.FromTownId).Single();
                    notification.ToTown = db.Districts.Where(x => x.DistrictId == notification.ToTownId).Single();
                }

                var response = new CancelNotificationResponse
                {
                    UserId = user.Id,
                    Notifications = notificationRequests
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

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
                var timeFlexibleCheck = Request["time-options-check"] == "on";
                var timeDisplay = Request["TimeDisplay"];
                var hourType = Request["hour-type"];

                var createdDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                var requestedFromDateTime = DateTime.SpecifyKind(tripDate, DateTimeKind.Local);
                var requestedToDateTime = DateTime.SpecifyKind(tripDate, DateTimeKind.Local);


                //get time by hour-type
                if (timeFlexibleCheck)
                {
                    switch (hourType)
                    {
                        case "Todo el día (12:00 am-11:59 pm)":
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 0, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 23, 59, 0);
                            break;
                        case "Madrugada (12:00 am-5:59 am)":
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 0, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 5, 59, 0);
                            break;
                        case "Mañana (6:00 am-11:59 am)":
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 6, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 11, 59, 0);
                            break;
                        case "Medio día (12:00 pm-12:59 pm)":
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 12, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 12, 59, 0);
                            break;
                        case "Tarde (1:00 pm-6:59 pm)":
                            requestedFromDateTime = new DateTime(requestedFromDateTime.Year, requestedFromDateTime.Month, requestedFromDateTime.Day, 13, 0, 0);
                            requestedToDateTime = new DateTime(requestedToDateTime.Year, requestedToDateTime.Month, requestedToDateTime.Day, 18, 59, 0);
                            break;
                        case "Noche (7:00 pm-11:59 pm)":
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
                    DateTime userSelectedTime = DateTime.Parse(timeDisplay);

                    userSelectedTime = new DateTime(tripDate.Year, tripDate.Month, tripDate.Day, userSelectedTime.Hour, userSelectedTime.Minute, 0);

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

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Info,
                    Message = user.UserType + " " + user.FullName + " ha creado una notificación automática para un viaje desde " + fromDistrict.FullName + " hasta " + toDistrict.FullName + "con un rango desde " +
                    requestedFromDateTime.ToString(WebConfigurationManager.AppSettings["TimeFormat"]) + " a las " + requestedToDateTime.ToString(WebConfigurationManager.AppSettings["TimeFormat"]),
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now),
                    UserEmail = User.Identity.Name
                }, logo);

                return RedirectToAction("Index", new { message = "100040" });
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

        [HttpPost]
        public string CancelNotification(int notificationRequestId, string userId)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                var notification = db.NotificationRequests
                    .Where(x => x.NotificationRequestId == notificationRequestId)
                    .Where(x => x.Status == Enums.RequestNotificationStatus.Active)
                    .SingleOrDefault();

                if (notification != null)
                {
                    notification.Status = Enums.RequestNotificationStatus.Cancelled;

                    db.Entry(notification).State = EntityState.Modified;
                    db.SaveChanges();
                }

                var notificationRequests = db.NotificationRequests
                    .Where(x => x.UserId == userId)
                    .Where(x => x.Status == Enums.RequestNotificationStatus.Active)
                    .Include(n => n.Reservation)
                    .ToList();

                foreach (var not in notificationRequests)
                {
                    not.RequestedFromDateTime = Common.ConvertToLocalTime(not.RequestedFromDateTime);
                    not.RequestedToDateTime = Common.ConvertToLocalTime(not.RequestedToDateTime);

                    not.FromTown = db.Districts.Where(x => x.DistrictId == not.FromTownId).Single();
                    not.ToTown = db.Districts.Where(x => x.DistrictId == not.ToTownId).Single();
                }

                var response = new CancelNotificationResponse
                {
                    //¡Notificación automática cancelada!
                    Message = "100041",
                    UserId = userId,
                    Notifications = notificationRequests,
                };

                var html = Serializer.RenderViewToString(this.ControllerContext, "Partials/p_Index", response);

                response.Html = html;

                return Serializer.Serialize(response);

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

                var notificationRequests = db.NotificationRequests
                   .Where(x => x.UserId == userId)
                   .Include(n => n.Reservation)
                   .ToList();

                foreach (var not in notificationRequests)
                {
                    not.RequestedFromDateTime = Common.ConvertToLocalTime(not.RequestedFromDateTime);
                    not.RequestedToDateTime = Common.ConvertToLocalTime(not.RequestedToDateTime);

                    not.FromTown = db.Districts.Where(x => x.DistrictId == not.FromTownId).Single();
                    not.ToTown = db.Districts.Where(x => x.DistrictId == not.ToTownId).Single();
                }

                var response = new CancelNotificationResponse
                {
                    Notifications = notificationRequests,
                    UserId = userId,
                    Html = Serializer.RenderViewToString(this.ControllerContext, "_Index", notificationRequests)
                };

                return Serializer.Serialize(response);
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
