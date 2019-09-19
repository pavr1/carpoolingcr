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

        public static void SendUserLogin(string userName, UserType userType, string userEmail, string appLogo)
        {
            var html = "¡El " + userType.ToString() + " " + userName + " se ha logueado en el sistema!";
            
            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "¡Usuario Logueado!",
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

        public static void SendEmail(int line, Enums.LogLocation location, Enums.LogType logType, string message, string method, DateTime time, string user, string fields, string appLogo)
        {
            string callbackUrl = "www.buscoridecr.com/Logs/Index";

            string dataType = string.Empty;
            string title = string.Empty;
            EmailType emailType = EmailType.Notifications;

            switch (logType)
            {
                case LogType.Info:
                    dataType = "información";
                    title = "Correo Informativo";
                    emailType = EmailType.Notifications;
                    break;
                case LogType.Error:
                    dataType = "error";
                    title = "Correo Informativo (Error)";
                    emailType = EmailType.Errors;
                    break;
                case LogType.Warning:
                    dataType = "advertencia";
                    title = "Correo Informativo (Advertencia)";
                    emailType = EmailType.Notifications;
                    break;
                case LogType.SMS:
                    dataType = "información sobre SMS";
                    title = "Correo Informativo (SMS)";
                    emailType = EmailType.Notifications;
                    break;
                case LogType.UserIdVerification:
                    dataType = "información sobre cédula del ususario";
                    title = "Correo Informativo (Actualización de cédula)";
                    emailType = EmailType.Notifications;
                    break;
                case LogType.VehicleCreation:
                    dataType = "información sobre vehículo del conductor";
                    title = "Correo Informativo (Registro de vehículo)";
                    emailType = EmailType.Notifications;
                    break;
                default:
                    title = "Correo Informativo";
                    emailType = EmailType.Notifications;
                    break;
            }

            var html = "¡Hemos recibido " + dataType + " en el sistema! Abajo detallamos los datos: <br/><br/>";
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
                Subject = title,
                Body = html
            }, emailType, appLogo);
        }

        private static void SendEmail(IdentityMessage msg, EmailType EmailType, string logo)
        {
            using (FileStream stream = System.IO.File.Open(logo, FileMode.Open, FileAccess.Read))
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

        public static void SendTripNotification(string email, string date,  string tripInfo, string callbackUrl, string appLogo)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "¡Hemos encontrado al menos un viaje que te puede servir el " + date  + " de " + tripInfo + "!<br/><br/> Para ver la información y poder reservar da click <b><a href='" + callbackUrl + "'>aquí</a></b>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "¡Notificación automática de viajes!",
                Body = html
            }, EmailType.Notifications, appLogo);
        }

        public static void SendEmailVerification(string email, bool isUseridentificationVerified, string appLogo)
        {
            var html = string.Empty;
            var title = string.Empty;

            if (isUseridentificationVerified)
            {
                title = "¡Número de cédula verificada!";
                html = "¡Felicidades! Tu número de cédula fue verificado por buscoridecr.com. Con esto generas más confianza entre los usuarios del sitio";
            }
            else
            {
                title = "¡Número de cédula inválida!";
                html = "¡Lo sentimos! Parece que el número de cédula brindado no concuerda con el nombre de usuario registrado. Por favor entre a su perfíl e ingrese el número de cédula que concuerde con su nombre.<br/><br/>";
                html += "Recuerde que buscoridecr.com hace la verificación de existencia de número de cédula por medio del Tribunal Supremo de Elecciones, por lo cual la cédula debe concordar con el nombre del usuario brindado.</br></br>";
                html += "Para más información por favor contacte al administrador a " + WebConfigurationManager.AppSettings["AdminEmails"];
            }

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Verificación de Cédula",
                Body = html
            }, EmailType.Notifications, appLogo);
        }
    }
}