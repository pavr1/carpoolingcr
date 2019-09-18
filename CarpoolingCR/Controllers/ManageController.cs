using CarpoolingCR.Models;
using CarpoolingCR.Models.Vehicle;
using CarpoolingCR.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CarpoolingCR.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                ViewBag.StatusMessage =
                    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                    : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                    : message == ManageMessageId.Error ? "An error has occurred."
                    : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                    : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                    : "";

                var user = Common.GetUserByEmail(User.Identity.Name);

                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;

                user.MonthlyBalance = db.MonthlyBalances.Where(x => x.ApplicationUserId == user.Id && x.Month == month && x.Year == year)
                    .SingleOrDefault();

                if (user.MonthlyBalance != null)
                {
                    user.MonthlyBalance.MonthlyTransactions = db.MonthlyTransactions.Where(x => x.MonthlyBalanceId == user.MonthlyBalance.MonthlyBalanceId)
                        .ToList();

                    ViewBag.Balance = 0;

                    if (user.MonthlyBalance.MonthlyTransactions.Count > 0)
                    {
                        ViewBag.Balance = user.MonthlyBalance.MonthlyTransactions[user.MonthlyBalance.MonthlyTransactions.Count - 1].FinalBalance;
                    }
                }

                if (user.BankAccountId != null)
                {
                    user.BankAccount = db.BankAccounts.Where(x => x.BankId == user.BankAccountId).SingleOrDefault();
                }

                ViewBag.BankId = new SelectList(db.Banks, "BankId", "BankName");

                var model = new IndexViewModel
                {
                    HasPassword = HasPassword(),
                    PhoneNumber = await UserManager.GetPhoneNumberAsync(user.Id),
                    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(user.Id),
                    Logins = await UserManager.GetLoginsAsync(user.Id),
                    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(user.Id),
                    User = user,
                    ProfileHtml = Serializer.RenderViewToString(this.ControllerContext, "Partials/_ProfileInfo", user)
                };

                return View(model);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public string SendMobileVerificationCode(string unneededParam)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                var user = Common.GetUserByEmail(User.Identity.Name);

                var smsServiceEnabled = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSMS"]);

                if (smsServiceEnabled)
                {
                    SMSHandler.SendSMS(user, "Código de Verificación: " + user.MobileVerficationNumber + ". ¡Hagamos Ride!", "www.buscoridecr.com", logo);

                    //¡Código de verificación enviado!
                    return "100042";
                }
                else
                {
                    Common.LogData(new Log
                    {
                        Line = Common.GetCurrentLine(),
                        Location = Enums.LogLocation.Server,
                        LogType = Enums.LogType.SMS,
                        Message = "El " + user.UserType + " " + user.FullName + ", email: " + user.Email + " no pudo verificar su número telefónico. Detalle: Servicio SMS inactivo",
                        Method = Common.GetCurrentMethod(),
                        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        UserEmail = User.Identity.Name
                    }, logo);

                    //¡Servicio SMS inactivo!
                    return "100048";
                }
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                //¡Código no enviado, contacte al administrador!
                return "100043";
            }
        }

        [HttpPost]
        public string ConfirmMobileVerificationCode(int verificationCode)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                var user = Common.GetUserByEmail(User.Identity.Name);

                if (user.MobileVerficationNumber == verificationCode)
                {
                    user.IsPhoneVerified = true;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    Common.LogData(new Log
                    {
                        Line = Common.GetCurrentLine(),
                        Location = Enums.LogLocation.Server,
                        LogType = Enums.LogType.SMS,
                        Message = "El " + user.UserType + " " + user.FullName + ", email: " + user.Email + " verificó su número telefónico.",
                        Method = Common.GetCurrentMethod(),
                        Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                        UserEmail = User.Identity.Name
                    }, logo);

                    return "true";
                }
                else
                {
                    return "false";
                }
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return "false";
            }
        }

        public ActionResult ProfileInfo(string id, string message)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg");

            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Warning = message;
                }

                var user = Common.GetUserByEmail(User.Identity.Name);

                if (user.MobileVerficationNumber == 0)
                {
                    user.MobileVerficationNumber = Common.GetRandomPhoneVerificationNumber();

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                ViewBag.BrandId = new SelectList(user.Brands, "BrandId", "Name");

                if (user.Vehicle != null)
                {
                    var brand = db.Brands.Where(x => x.BrandId == user.Vehicle.BrandId).Single();

                    ViewBag.ModelId = new SelectList(brand.Models, "ModelId", "Description");
                }

                ViewBag.BrandJson = Serializer.Serialize(user.BrandsJson);

                return View(user);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View();
            }
        }

        [HttpPost]
        //public string ProfileInfo(string name, string lastName, string secLastName, string phone1, string phone2, string picture)
        public ActionResult ProfileInfo(string id)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            var fields = "Fields => ";

            var user = Common.GetUserByEmail(User.Identity.Name);

            try
            {
                if (!Common.IsAuthorized(User))
                {
                    return RedirectToAction("Login", "Account");
                }

                ViewBag.BrandId = new SelectList(user.Brands, "BrandId", "Name");
                ViewBag.BrandJson = Serializer.Serialize(user.BrandsJson);

                #region Fields
                fields += "UserIdentification: " + Request["UserIdentification"];
                fields += "Name: " + Request["Name"];
                fields += "LastName: " + Request["LastName"] + ", ";
                fields += "SecondLastName: " + Request["SecondLastName"] + ", ";
                fields += "Phone1: " + Request["Phone1"] + ", ";
                fields += "Phone2: " + Request["Phone2"] + ", ";
                #endregion

                if (user != null)
                {
                    string path = string.Empty;

                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fnSplit = Path.GetFileName(file.FileName).Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            var fileName = "Profile_" + user.Name + "_" + user.LastName + "_" + user.SecondLastName + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "." + fnSplit[1];

                            string relativePath = "~/Content/Pictures/Users/" + fileName;
                            string absolutePath = Server.MapPath(relativePath);

                            try
                            {
                                file.SaveAs(absolutePath);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error saving image. Relative Path: " + relativePath + ". Absolute Path: " + absolutePath + ". Error: " + ex.Message);
                            }

                            path = relativePath;

                            //path = Path.Combine(Server.MapPath("~/Content/Pictures/Users"), fileName);
                            //file.SaveAs(path);

                            //path = "\\Content\\" + path.Split(new string[] { "\\Content\\" }, StringSplitOptions.RemoveEmptyEntries)[1];
                            //path = "~/" + path.Replace("\\", "/");
                        }
                    }
                    if (string.IsNullOrEmpty(Request["UserIdentification"]))
                    {
                        //¡Cédula Requerida!
                        ViewBag.Warning = "100049";
                        return View(user);
                    }
                    if (string.IsNullOrEmpty(Request["Name"]))
                    {
                        //Nombre Requerido
                        ViewBag.Warning = "100030";
                        return View(user);
                    }
                    if (string.IsNullOrEmpty(Request["LastName"]))
                    {
                        //Apellido Requerido
                        ViewBag.Warning = "100031";
                        return View(user);
                    }
                    if (string.IsNullOrEmpty(Request["Phone1"]))
                    {
                        //Celular Requerido
                        ViewBag.Warning = "100032";
                        return View(user);
                    }

                    if (!user.IsUserIdentificationVerified)
                    {
                        if (user.UserIdentification != Request["UserIdentification"])
                        {
                            Common.LogData(new Log
                            {
                                Line = Common.GetCurrentLine(),
                                Location = Enums.LogLocation.Server,
                                LogType = Enums.LogType.UserIdVerification,
                                Message = "El " + user.UserType + " " + user.FullName + " ha ingresado su número de cédula " + Request["UserIdentification"] + ". Proceda a validar esta información en el sitio https://www.tse.go.cr/consulta_persona/consulta_cedula.aspx",
                                Method = Common.GetCurrentMethod(),
                                Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                                UserEmail = User.Identity.Name,
                                Fields = fields
                            }, logo);
                        }
                    }

                    user.UserIdentification = Request["UserIdentification"];
                    user.Name = Request["Name"];
                    user.LastName = Request["LastName"];
                    user.SecondLastName = Request["SecondLastName"];
                    user.Phone1 = Request["Phone1"];
                    user.Phone2 = Request["Phone2"];

                    if (!string.IsNullOrEmpty(path))
                    {
                        user.Picture = path;
                    }

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    if ((user.UserType == Enums.UserType.Conductor) || (user.UserType == Enums.UserType.Administrador))
                    {
                        #region Fields
                        fields += "Información del vehículo";
                        fields += "BrandId: " + Request["BrandId"];
                        fields += "Model: " + Request["Model"] + ", ";
                        fields += "Color: " + Request["Color"] + ", ";
                        fields += "Plate: " + Request["Plate"] + ", ";
                        fields += "Capacity: " + Request["Capacity"] + ", ";
                        #endregion

                        var brand = Request["BrandId"];
                        var model = Request["Model"];
                        var color = Request["Color"];
                        var plate = Request["Plate"];
                        var capacity = Request["Capacity"];

                        var foundBrand = db.Brands.Where(x => x.Name == brand).SingleOrDefault();
                        Model foundModel = null;

                        user.Vehicle = db.Vehicles.Where(x => x.ApplicationUserId == user.Id).SingleOrDefault();
                        var vehicleExistent = (user.Vehicle != null);

                        if (!vehicleExistent)
                        {
                            user.Vehicle = new Vehicle();
                        }

                        if (foundBrand == null)
                        {
                            ViewBag.Warning = "100028";

                            return View(user);
                        }
                        else
                        {
                            user.Vehicle.Brand = foundBrand;
                            user.Vehicle.BrandId = foundBrand.BrandId;

                            ViewBag.BrandId = new SelectList(user.Brands, "BrandId", "Name", foundBrand.BrandId);
                            ViewBag.ModelId = new SelectList(foundBrand.Models, "ModelId", "Description");

                            foundModel = db.Models.Where(x => x.Description == model && x.BrandId == foundBrand.BrandId).SingleOrDefault();

                            if (foundModel == null)
                            {
                                ViewBag.Warning = "100029";

                                return View(user);
                            }
                            else
                            {
                                user.Vehicle.Model = foundModel;
                                user.Vehicle.ModelId = foundModel.ModelId;
                            }
                        }

                        if (string.IsNullOrEmpty(plate))
                        {
                            ViewBag.Warning = "100034";
                            return View(user);
                        }
                        else
                        {
                            user.Vehicle.Plate = plate;
                        }

                        if (string.IsNullOrEmpty(color))
                        {
                            ViewBag.Warning = "100033";

                            return View(user);
                        }
                        else
                        {
                            user.Vehicle.Color = color;
                        }

                        if (string.IsNullOrEmpty(capacity))
                        {
                            ViewBag.Warning = "100035";
                            return View(user);
                        }
                        else
                        {
                            user.Vehicle.Spaces = Convert.ToInt32(capacity);
                        }

                        if (!vehicleExistent)
                        {
                            user.Vehicle.ApplicationUser = user;
                            user.Vehicle.ApplicationUserId = user.Id;

                            var newVehicle = user.Vehicle;
                            newVehicle.Brand = null;
                            newVehicle.Model = null;
                            newVehicle.ApplicationUser = null;

                            db.Entry(newVehicle).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            db.Entry(user.Vehicle).State = System.Data.Entity.EntityState.Modified;
                        }

                        db.SaveChanges();
                    }

                    //¡Perfíl Actualizado!
                    ViewBag.Info = "100011";
                }

                //return Serializer.RenderViewToString(this.ControllerContext, "Partials/_ProfileInfo", user);
                return View(user);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name,
                    Fields = fields
                }, logo);

                user.Message = "Error inesperado, intente de nuevo!";
                user.MessageType = "error";

                //return Serializer.RenderViewToString(this.ControllerContext, "Partials/_ProfileInfo", user);
                return View(user);
            }
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View(model);
            }
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            var logo = Server.MapPath("~/Content/Icons/ride_small - Copy.jpg"); ;

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
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
                    Timestamp = Common.ConvertToUTCTime(DateTime.Now.ToLocalTime()),
                    UserEmail = User.Identity.Name
                }, logo);

                ViewBag.Error = "¡Error inesperado, intente de nuevo!";

                return View(model);
            }
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}