using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;

namespace CarpoolingCR.Utils
{
    public class EmailHandler
    {
        public static void SendEmailConfirmation(string callbackUrl, string email)
        {
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
            var html = "<html><header></header><body>Para resetear la clave, de click <a href='" + callbackUrl + "'>AQUÍ</a></body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = email,
                Subject = "Reseteo de clave",
                Body = html
            });
        }

        public static void SendEmailNewTown()
        {
            var html = "<html><header></header><body>Un usuario ha creado una nueva localidad, la cuál está pendiente de aprovación. Da click ac.</body></html>";// de click <a href='" + callbackUrl + "'>AQUÍ</a></body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Nueva localidad creada",
                Body = html
            });
        }

        public static void SendReservationStatusChange(string email, string trip, string date, string status)
        {
            var html = "<html><header></header><body>La reservación para el viaje " + trip + " el " + date + " ha sido " + status + ". Para ver más información da click AQUí.</body></html>";// de click <a href='" + callbackUrl + "'>AQUÍ</a></body></html>";

            Common.SendEmail(new IdentityMessage
            {
                Destination = WebConfigurationManager.AppSettings["AdminEmails"],
                Subject = "Nueva localidad creada",
                Body = html
            });
        }
    }
}