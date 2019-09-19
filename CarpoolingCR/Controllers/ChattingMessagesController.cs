using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class ChattingMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChattingMessages
        public string Index(int reservationId)
        {
            var user = Common.GetUserByEmail(User.Identity.Name);
            var reservation = db.Reservations.Where(x => x.ReservationId == reservationId).SingleOrDefault();
            var targetUserId = string.Empty;

            if (user.Id == reservation.ApplicationUserId)
            {
                var trip = db.Trips.Where(x => x.TripId == reservation.TripId).SingleOrDefault();

                if (trip != null)
                {
                    targetUserId = trip.ApplicationUserId;
                }
            }
            else
            {
                targetUserId = reservation.ApplicationUserId;
            }

            var response = new CarpoolingCR.Objects.Responses.ChatMessageResponse
            {
                Messages = db.ChattingMessages.Where(x => x.ReservationId == reservationId).ToList(),
                CurrentUserId = user.Id,
                ReservationId = reservationId
            };

            return Serializer.RenderViewToString(this.ControllerContext, "_Index", response);
        }

        // GET: ChattingMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChattingMessage chattingMessage = db.ChattingMessages.Find(id);
            if (chattingMessage == null)
            {
                return HttpNotFound();
            }
            return View(chattingMessage);
        }

        // GET: ChattingMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChattingMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public string Create(int? reservationId, string userId, string message)
        {
            var msg = new ChattingMessage
            {
                ReservationId = (int)reservationId,
                UserId = userId,
                Message = message,
                Date = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime())
            };

            var reservation = db.Reservations.Where(x => x.ReservationId == reservationId).SingleOrDefault();
            var targetUserId = string.Empty;

            if (userId == reservation.ApplicationUserId)
            {
                var trip = db.Trips.Where(x => x.TripId == reservation.TripId).SingleOrDefault();

                if (trip != null)
                {
                    targetUserId = trip.ApplicationUserId;
                }
            }
            else
            {
                targetUserId = reservation.ApplicationUserId;
            }

            db.ChattingMessages.Add(msg);
            db.SaveChanges();

            new SignalHandler().SendMessage(targetUserId, message);

            var response = new CarpoolingCR.Objects.Responses.ChatMessageResponse
            {
                Messages = db.ChattingMessages.Where(x => x.ReservationId == reservationId).ToList(),
                CurrentUserId = userId,
                ReservationId = (int)reservationId
            };

            return Serializer.RenderViewToString(this.ControllerContext, "_Index", response);
        }

        // GET: ChattingMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChattingMessage chattingMessage = db.ChattingMessages.Find(id);
            if (chattingMessage == null)
            {
                return HttpNotFound();
            }
            return View(chattingMessage);
        }

        // POST: ChattingMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChattingMessageId,ReservationId,FromName,ToName,Message,Date")] ChattingMessage chattingMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chattingMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chattingMessage);
        }

        // GET: ChattingMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChattingMessage chattingMessage = db.ChattingMessages.Find(id);
            if (chattingMessage == null)
            {
                return HttpNotFound();
            }
            return View(chattingMessage);
        }

        // POST: ChattingMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChattingMessage chattingMessage = db.ChattingMessages.Find(id);
            db.ChattingMessages.Remove(chattingMessage);
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
