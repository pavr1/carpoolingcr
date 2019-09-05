using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace CarpoolingCR.Controllers
{
    public class TripQuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TripQuestions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayMessages()
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                var questionInfos = db.TripQuestionInfos.Where(x => x.DriverId == user.Id || x.PassengerId == user.Id)
                    .Include(x => x.TripQuestions)
                    .ToList();

                foreach (var item in questionInfos)
                {
                    item.Driver = db.Users.Where(x => x.Id == item.DriverId).SingleOrDefault();
                    item.Passenger = db.Users.Where(x => x.Id == item.PassengerId).SingleOrDefault();
                    item.CurrentUserId = user.Id;
                    item.MessagesHtml = Serializer.RenderViewToString(this.ControllerContext, "Partials/_MessageHistory", item);
                }

                var response = new DisplayMessagesResponse
                {
                    ActualUserId = user.Id,
                    QuestionsInfo = questionInfos
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

        [HttpPost]
        public string SendQuestion(string driverId, string passengerId, int? tripQuestionInfoId, string message)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            var tran = db.Database.BeginTransaction();

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    RedirectToAction("Login", "Account");

                    return string.Empty;
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                TripQuestionInfo tripInfo = null;
                var infoID = -1;
                var currentTime = DateTime.Now;

                if(tripQuestionInfoId == null)
                {
                    var existentQuestionInfo = db.TripQuestionInfos.Where(x => x.DriverId == driverId && x.PassengerId == passengerId).SingleOrDefault();

                    if (existentQuestionInfo != null)
                    {
                        existentQuestionInfo.LastMessageSent = Common.ConvertToUTCTime(DateTime.Now);

                        db.Entry(existentQuestionInfo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        tripInfo = existentQuestionInfo;
                    }
                    else
                    {
                        tripInfo = new TripQuestionInfo
                        {
                            DriverId = driverId,
                            PassengerId = passengerId,
                            LastMessageSent = Common.ConvertToUTCTime(currentTime)
                        };

                        db.Entry(tripInfo).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }

                    infoID = tripInfo.TripQuestionInfoId;
                }
                else
                {
                    infoID = (int)tripQuestionInfoId;
                    tripInfo = db.TripQuestionInfos.Where(x => x.TripQuestionInfoId == infoID).SingleOrDefault();

                    tripInfo.LastMessageSent = Common.ConvertToUTCTime(currentTime);

                    db.Entry(tripInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }

                var question = new TripQuestion
                {
                    CurrentUserId = user.Id,
                    TripQuestionInfoId = infoID,
                    DateTime = Common.ConvertToUTCTime(currentTime),
                    Message = message
                };

                db.Entry(question).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                tran.Commit();

                return "¡Mensaje Enviado!";
            }
            catch (Exception ex)
            {
                tran.Rollback();

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

                return string.Empty;
            }
        }

        [HttpPost]
        public string SendQuestion2(string driverId, string passengerId, int? tripQuestionInfoId, string message)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");;

            var tran = db.Database.BeginTransaction();

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    RedirectToAction("Login", "Account");

                    return string.Empty;
                }

                var user = Common.GetUserByEmail(User.Identity.Name);
                TripQuestionInfo tripInfo = null;
                var infoID = -1;
                var currentTime = DateTime.Now;
                var existentQuestionInfo = new TripQuestionInfo();

                if (tripQuestionInfoId == null)
                {
                    existentQuestionInfo = db.TripQuestionInfos.Where(x => x.DriverId == driverId && x.PassengerId == passengerId).SingleOrDefault();

                    if (existentQuestionInfo != null)
                    {
                        existentQuestionInfo.LastMessageSent = Common.ConvertToUTCTime(DateTime.Now);

                        db.Entry(existentQuestionInfo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        tripInfo = existentQuestionInfo;
                    }
                    else
                    {
                        tripInfo = new TripQuestionInfo
                        {
                            DriverId = driverId,
                            PassengerId = passengerId,
                            LastMessageSent = Common.ConvertToUTCTime(currentTime)
                        };

                        db.Entry(tripInfo).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }

                    infoID = tripInfo.TripQuestionInfoId;
                }
                else
                {
                    infoID = (int)tripQuestionInfoId;
                    tripInfo = db.TripQuestionInfos.Where(x => x.TripQuestionInfoId == infoID).SingleOrDefault();

                    tripInfo.LastMessageSent = Common.ConvertToUTCTime(currentTime);

                    db.Entry(tripInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }

                var question = new TripQuestion
                {
                    CurrentUserId = user.Id,
                    TripQuestionInfoId = infoID,
                    DateTime = Common.ConvertToUTCTime(currentTime),
                    Message = message
                };

                db.Entry(question).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                tran.Commit();

                existentQuestionInfo = db.TripQuestionInfos.Where(x => x.DriverId == driverId && x.PassengerId == passengerId).SingleOrDefault();
                var html = Serializer.RenderViewToString(this.ControllerContext, "Partials/_MessageHistory", existentQuestionInfo);

                return html;
            }
            catch (Exception ex)
            {
                tran.Rollback();

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

                return string.Empty;
            }
        }
    }
}