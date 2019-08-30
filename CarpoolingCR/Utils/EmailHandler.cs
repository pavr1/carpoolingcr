using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Web.Configuration;

namespace CarpoolingCR.Utils
{
    public class EmailHandler
    {
        public static void SendEmailConfirmation(string callbackUrl, string email)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Gracias por su deseo de formar parte de nuestro sitio. buscoridecr.com le da la bienvenida! <br/> Por favor confirma tu cuenta dando click <a href='" + callbackUrl + "'><b>AQUÍ</a></a></body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Confirma tu correo electrónico",
                Body = html
            });
        }

        public static void SendEmailNewUserRegistered(ApplicationUser user, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var userInfo = "<br/>Nombre: " + user.Name + " " + user.LastName + " " + user.SecondLastName + "<br/>Correo: " + user.Email + "<br/>Contacto: " + user.Phone1 + " - " + user.Phone2;


            var html = "<html><header></header><body>Un nuevo " + user.UserType.ToString() + " ha sido registrado en el sistema! <br/><br/>Información de usuario: " + userInfo + "<br/><br/><a href='" + callbackUrl + "'><b>Ver datos de usuario</a></a></body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Nuevo usuario registrado",
                Body = html
            });
        }

        public static void SendEmailForgotPassword(string callbackUrl, string email)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Para resetear la contraseña, por favor de click <a href='" + callbackUrl + "'>AQUÍ</a></body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Reseteo de contraseña",
                Body = html
            });
        }

        public static void SendEmailNewTown(string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Un usuario ha creado una nueva localidad, la cuál está pendiente de aprobación. Por favor de click <a href='" + callbackUrl + "'>AQUÍ</a> para ir a localidades</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Nueva localidad creada",
                Body = html
            });
        }

        public static void HomePageHit()
        {
            var html = "<html><header></header><body>La página Home del sitio ha sido accesada</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "El sitio ha sido accesado",
                Body = html
            });
        }

        public static void SendReservationStatusChangeByDriver(string email, string trip, string date, string status, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Tu reservación para el viaje " + trip + " el " + date + " ha sido " + status + " por el conductor. Para ver más información por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>.</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Reservación " + status,
                Body = html
            });
        }

        public static void SendReservationStatusCancelledByPassenger(string email, string trip, string date, int spaces, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Tu reservación para el viaje " + trip + " el " + date + ", de " + spaces + " espacios, ha sido cancelada por el pasajero. Para ver más información por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>.</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Reservación Cancelada",
                Body = html
            });
        }

        public static void SendTripsCancelledByDriver(string email, string trip, string date, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Lo sentimos, parece que el viaje a " + trip + " el " + date + " ha sido cancelado por el conductor. Para ver otras opciones de viaje, por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>.</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Viaje Cancelado",
                Body = html
            });
        }

        public static void SendEmailTripReservation(string adminEmail, string email, string passengerName, int spaces, string tripInfo, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>" + passengerName + " ha solicitado " + spaces + " espacios para tu viaje de " + tripInfo + "<br/><br/>Da click <b><a href='" + callbackUrl + "'>aquí</a></b> para ver la reserva!</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "¡Han solicitado espacio en tu vehículo!",
                Body = html
            });

            html = "<html><header></header><body>" + passengerName + " ha solicitado " + spaces + " espacios en un viaje</body></html>";

            SendEmail(new IdentityMessage
            {
                Destination = adminEmail,
                Subject = "¡Han creado una reservación!",
                Body = html
            });
        }

        [Obsolete]
        public static void SendEmailTripCreation(string email, string driverName, string tripInfo, int availableSpaces, string callback)
        {
            var html = "<html><header></header><body>El conductor " + driverName + " ha creado un viaje de " + tripInfo + " con " + availableSpaces + " espacios disponibles.";

            SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "¡Nuevo viaje disponible!",
                Body = html
            });

            var sendEmails = new Thread(() => SendToAll(tripInfo, callback));
            sendEmails.Start();
        }

        private static void SendToAll(string tripInfo, string callback)
        {
            using (var db = new ApplicationDbContext())
            {
                var users = db.Users.Where(x => x.EmailConfirmed && x.Status == Enums.ProfileStatus.Active)
                    .Where(x => x.UserType == Enums.UserType.Pasajero)
                    .ToList();

                var html = "<html><header></header><body>Nuevo viaje disponible de " + tripInfo + ". ¿Necesitas ride? ¡Aprovecha y <a href='" + callback + "'>Reserva Aquí</a>!</body></html>";

                foreach (var user in users)
                {
                    SendEmail(new IdentityMessage
                    {
                        Destination = user.Email,
                        Subject = "¡Nuevo viaje disponible!",
                        Body = html
                    });
                }
            }
        }

        private static void SendEmail(IdentityMessage msg)
        {
            new EmailService().SendAsync(msg);
        }
    }
}