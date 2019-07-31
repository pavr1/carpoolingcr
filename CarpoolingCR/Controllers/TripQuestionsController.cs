﻿using CarpoolingCR.Models;
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
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        [HttpPost]
        public string SendQuestion(string driverId, string passengerId, int? tripQuestionInfoId, string message)
        {
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

                if(tripQuestionInfoId == null)
                {
                    tripInfo = new TripQuestionInfo {
                        DriverId = driverId,
                        PassengerId = passengerId,
                    };

                    db.Entry(tripInfo).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();

                    infoID = tripInfo.TripQuestionInfoId;
                }
                else
                {
                    infoID = (int)tripQuestionInfoId;
                }

                var question = new TripQuestion
                {
                    CurrentUserId = user.Id,
                    TripQuestionInfoId = infoID,
                    DateTime = Common.ConvertToUTCTime(DateTime.Now),
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
                });

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return string.Empty;
            }
        }
    }
}