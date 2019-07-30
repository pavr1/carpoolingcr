using CarpoolingCR.Models;
using CarpoolingCR.Objects.Responses;
using CarpoolingCR.Utils;
using System;
using System.Linq;
using System.Web.Mvc;

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
                var messages = db.TripQuestions.Where(x => x.FromId == user.Id || x.toId == user.Id).ToList();

                var response = new DisplayMessagesResponse
                {
                    ActualUserId = user.Id,
                    Messages = messages
                };

                return View(messages);
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
        public string SendQuestion(string driverId, string message)
        {
            try
            {
                if (!Common.IsAuthorized(User))
                {
                    RedirectToAction("Login", "Account");

                    return string.Empty;
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                var question = new TripQuestion
                {
                    FromId = user.Id,
                    toId = driverId,
                    DateTime = Common.ConvertToUTCTime(DateTime.Now),
                    Message = message
                };

                db.Entry(question).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                return "¡Mensaje Enviado!";
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

                return string.Empty;
            }
        }
    }
}