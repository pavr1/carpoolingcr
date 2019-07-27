using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;

namespace CarpoolingCR.Utils
{
    public class EmailHandler
    {
        public static void SendEmailConfirmation(string callbackUrl, string email)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Gracias por querer formar parte de nuestro grupo. Buscoridecr.com le da la bienvenida! <br/> Por favor confirma tu cuenta dando click <a href='" + callbackUrl + "'><b>AQUÍ</a></a></body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Confirma tu correo electrónico",
                Body = html
            });
        }

        public static void SendEmailNewUserRegistered(ApplicationUser user, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var userInfo = "Nombre: " + user.Name + " " + user.LastName + " " + user.SecondLastName + "<br/>Correo: " + user.Email + "<br/>Teléfono: " + user.Phone1 + " - " + user.Phone2;


            var html = "<html><header></header><body>Un nuevo " + user.UserType.ToString() + " ha sido registrado en el sistema! <br/><a href='" + callbackUrl + "'><b>Ver datos de usuario</a></a></body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Nuevo usuario registrado",
                Body = html
            });
        }

        public static void SendEmailForgotPassword(string callbackUrl, string email)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Para resetear la clave, por favor de click <a href='" + callbackUrl + "'>AQUÍ</a></body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Reseteo de clave",
                Body = html
            });
        }

        public static void SendEmailNewTown(string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Un usuario ha creado una nueva localidad, la cuál está pendiente de aprobación. Por favor de click <a href='" + callbackUrl + "'>AQUÍ</a> para ir a localidades</body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Nueva localidad creada",
                Body = html
            });
        }

        public static void SendReservationStatusChangeByDriver(string email, string trip, string date, string status, string callbackUrl)
        {
            callbackUrl = callbackUrl.Replace("http://", "https://");

            var html = "<html><header></header><body>Tu reservación para el viaje " + trip + " el " + date + " ha sido " + status + " por el conductor. Para ver más información por favor de click <a href='" + callbackUrl + "'>AQUÍ</a>.</body></html>";

            Common.SendEmail(new IdentityMessage
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

            Common.SendEmail(new IdentityMessage
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

            Common.SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Viaje Cancelado",
                Body = html
            });
        }
    }
}