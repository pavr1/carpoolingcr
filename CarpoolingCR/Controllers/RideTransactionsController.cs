using CarpoolingCR.Models;
using CarpoolingCR.Utils;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    public class RideTransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RideTransactions
        public ActionResult Index()
        {
            return View(db.RideTransactions.ToList());
        }

        // GET: RideTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RideTransaction rideTransaction = db.RideTransactions.Find(id);
            if (rideTransaction == null)
            {
                return HttpNotFound();
            }
            return View(rideTransaction);
        }

        // GET: RideTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RideTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string referenceNumber, decimal amount, string transactionType, string purchasedBank, string purchasedSavingAccount, string purchasedSinpeAccount)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                var tT = Utils.Enums.RideTransactionTypeEnum.Deposit;

                if (!Enum.TryParse(transactionType, out tT))
                {
                    throw new Exception("Invalid TransationType value: " + transactionType);
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                byte[] imageData = null;

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        MemoryStream target = new MemoryStream();
                        file.InputStream.CopyTo(target);
                        imageData = target.ToArray();
                    }
                }

                if (imageData == null)
                {
                    //pop up error telling the user to upload transaction image
                    return View();
                }

                var rideTransaction = new RideTransaction
                {
                    ReferencedNumber = referenceNumber,
                    Amount = amount,
                    RequestedDate = DateTime.Now,
                    TransactionStatus = Utils.Enums.RideTransactionStatusEnum.Pending,
                    TransactionType = tT,
                    UserId = user.Id,
                    Image = imageData,
                    PurchasedBank = purchasedBank,
                    PurchasedSavingsAccount = purchasedSavingAccount,
                    PurchasedSinpeAccount = purchasedSinpeAccount
                };

                var callbackUrl = Url.Action("Index", "RideTransactions", new { }, protocol: Request.Url.Scheme);

                var send = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendNotificationsToAdmin"]);

                if (send)
                {
                    EmailHandler.SendEmailRidecoinsRequest(callbackUrl, user, rideTransaction.ReferencedNumber, rideTransaction.Amount, rideTransaction.TransactionType, logo);
                }

                return View();
            }
            catch (Exception ex)
            {
                var inner = (ex.InnerException != null) ? ex.InnerException.Message : "None";

                Common.LogData(new Log
                {
                    Line = Common.GetCurrentLine(),
                    Location = Enums.LogLocation.Server,
                    LogType = Enums.LogType.Error,
                    Message = ex.Message + " / Inner: " + inner + " / " + ex.StackTrace,
                    Method = Common.GetCurrentMethod(),
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        // GET: RideTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RideTransaction rideTransaction = db.RideTransactions.Find(id);
            if (rideTransaction == null)
            {
                return HttpNotFound();
            }
            return View(rideTransaction);
        }

        // POST: RideTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RideTransactionId,UserId,RequestedDate,TransactionType,TransactionStatus,AppliedDate,ReferencedNumber,Image,Amount,Detail")] RideTransaction rideTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rideTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rideTransaction);
        }

        // GET: RideTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RideTransaction rideTransaction = db.RideTransactions.Find(id);
            if (rideTransaction == null)
            {
                return HttpNotFound();
            }
            return View(rideTransaction);
        }

        // POST: RideTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RideTransaction rideTransaction = db.RideTransactions.Find(id);
            db.RideTransactions.Remove(rideTransaction);
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
