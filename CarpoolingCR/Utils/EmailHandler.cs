using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using static CarpoolingCR.Utils.Enums;

namespace CarpoolingCR.Utils
{
    public class EmailHandler
    {
        public static void SendEmailConfirmation(string callbackUrl, string email, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "Gracias por formar parte de nuestro sitio. Buscoridecr.com le da la bienvenida! <br/> Para confirmar tu cuenta da click <a href='" + callbackUrl + "'><b>AQUÍ</a></a>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Confirmación de correo electrónico",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendEmailNewUserRegistered(ApplicationUser user, string callbackUrl, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var userInfo = "<br/>Nombre: " + user.Name + " " + user.LastName + " " + user.SecondLastName + "<br/>Correo: " + user.Email + "<br/>Contacto: " + user.Phone1 + " - " + user.Phone2;


            var html = "Un nuevo " + user.UserType.ToString() + " ha sido registrado en el sistema! <br/><br/>Información de usuario: " + userInfo + "<br/><br/><a href='" + callbackUrl + "'><b>Ver datos de usuario</a></a>";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "¡Nuevo usuario registrado!",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendEmailForgotPassword(string callbackUrl, string email, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "Para reiniciar la contraseña, por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Reinicio de contraseña",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        //public static void SendEmailNewTown(string callbackUrl)
        //{
        //    callbackUrl = callbackUrl.Replace("http://", "https://");

        //    var html = "<html><header></header><body>Un usuario ha creado una nueva localidad, la cuál está pendiente de aprobación. Por favor de click <a href='" + callbackUrl + "'>AQUÍ</a> para ir a localidades</body></html>";

        //    SendEmail(new IdentityMessage
        //    {
        //        Destination = WebConfigurationManager.AppSettings["AdminEmails"],
        //        Subject = "Nueva localidad creada",
        //        Body = html
        //    }, EmailType.Notifications);
        //}

        public static void HomePageHit(string appLogo)
        {
            var html = "La página Home del sitio ha sido accesada";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "El sitio ha sido accesado",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendReservationStatusChangeByDriver(string email, string trip, string date, string status, string callbackUrl, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "Tu reservación para el viaje " + trip + " el " + date + " ha sido " + status + " por el conductor. Para ver más información por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>.";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "¡Reservación " + status + "!",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendReservationStatusCancelledByPassenger(string email, string trip, string date, int spaces, string callbackUrl, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "Tu reservación para el viaje " + trip + " el " + date + ", de " + spaces + " espacios, ha sido cancelada por el pasajero. Para ver más información por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>.";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "¡Reservación Cancelada!",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendTripsCancelledByDriver(string email, string trip, string date, string callbackUrl, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "Lo sentimos, parece que el viaje a " + trip + " el " + date + " ha sido cancelado por el conductor. Para ver otras opciones de viaje, por favor da click <a href='" + callbackUrl + "'>AQUÍ</a>.";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "¡Viaje Cancelado!",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendEmailTripReservation(string adminEmail, string email, string passengerName, int spaces, string tripInfo, string callbackUrl, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = passengerName + " ha solicitado " + spaces + " espacios para tu viaje de " + tripInfo + "<br/><br/>Da click <b><a href='" + callbackUrl + "'>aquí</a></b> para ver la solicitud de reservación!";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "¡Han solicitado ride para tu viaje!",
                Body = html
            }, EmailType.Notifications, appLogo);

            html = passengerName + " ha solicitado " + spaces + " espacios en un viaje de " + tripInfo;

            SendEmail(new IdentityMessage
            {
                Destination = adminEmail,
                Subject = "¡Han creado una reservación!",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        //[Obsolete]
        //public static void SendEmailTripCreation(string email, string driverName, string tripInfo, int availableSpaces, string callback)
        //{
        //    var html = "<html><header></header><body>El conductor " + driverName + " ha creado un viaje de " + tripInfo + " con " + availableSpaces + " espacios disponibles.";

        //    SendEmail(new IdentityMessage
        //    {
        //        Destination = email,
        //        Subject = "¡Nuevo viaje disponible!",
        //        Body = html
        //    }, EmailType.Notifications);

        //    var sendEmails = new Thread(() => SendToAll(tripInfo, callback));
        //    sendEmails.Start();
        //}

        //private static void SendToAll(string tripInfo, string callback)
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var users = db.Users.Where(x => x.EmailConfirmed && x.Status == Enums.ProfileStatus.Active)
        //            .Where(x => x.UserType == Enums.UserType.Pasajero)
        //            .ToList();

        //        var html = "<html><header></header><body>Nuevo viaje disponible de " + tripInfo + ". ¿Necesitas ride? ¡Aprovecha y <a href='" + callback + "'>Reserva Aquí</a>!</body></html>";

        //        foreach (var user in users)
        //        {
        //            SendEmail(new IdentityMessage
        //            {
        //                Destination = user.Email,
        //                Subject = "¡Nuevo viaje disponible!",
        //                Body = html
        //            }, EmailType.Notifications);
        //        }
        //    }
        //}

        public static void SentdInformativeEmailsToAllUsers()
        {

        }

        public static void SendErrorEmail(int line, Enums.LogLocation location, Enums.LogType logType, string message, string method, DateTime time, string user, string fields, string appLogo)
        {
            string callbackUrl = "https://buscoridecr.com/Logs/Index";

            var html = "¡Hemos detectado un error en el sistema! Abajo detallamos los datos del error: <br/><br/>";
            html += "<b>Localización: </b>" + location + "<br/>";
            html += "<b>Tipo de Log: </b>" + logType + "<br/>";
            html += "<b>Descrición: </b>" + message + "<br/>";
            html += "<b>Método: </b>" + method + "<br/>";
            html += "<b>Línea: </b>" + line + "<br/>";
            html += "<b>Fecha: </b>" + time + "<br/>";
            html += "<b>Usuario: </b>" + user + "<br/>";
            html += "<b>Campos: </b>" + fields + "<br/><br/>";
            html += "Para ver más información da click <a href='" + callbackUrl + "'><b>AQUÍ</a></a>";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "¡Hemos encontrado un error!",
                Body = html
            }, EmailType.Errors, appLogo);
        }

        private static void SendEmail(IdentityMessage msg, EmailType EmailType, string logo)
        {
            using (FileStream stream = System.IO.File.Open(logo, FileMode.Open))
            {
                var providerEmail = string.Empty;
                var providerPwd = string.Empty;

                switch (EmailType)
                {
                    case EmailType.Notifications:
                        providerEmail = WebConfigurationManager.AppSettings["NotificationsEmail"];
                        providerPwd = WebConfigurationManager.AppSettings["NotificationsPassword"];
                        break;
                    case EmailType.Updates:
                        //peding
                        break;
                    case EmailType.Errors:
                        providerEmail = WebConfigurationManager.AppSettings["ErrorsEmail"];
                        providerPwd = WebConfigurationManager.AppSettings["ErrorsPassword"];
                        break;
                    default:
                        providerEmail = WebConfigurationManager.AppSettings["NotificationsEmail"];
                        providerPwd = WebConfigurationManager.AppSettings["NotificationsPassword"];
                        break;
                }

                new EmailService().SendInformativeAsync(msg.Destination, msg.Subject, msg.Body, "logo", stream, "image/jpg", null, null, providerEmail, providerPwd, logo);
            }
        }
    }
}