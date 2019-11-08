using CarpoolingCR.Models;
using Microsoft.AspNet.Identity;

namespace CarpoolingCR.Utils
{
    public class SMSHandler
    {
        public static string SendSMS(ApplicationUser user, string text, string url, string logo, out string type)
        {
            type = string.Empty;

            var message = new IdentityMessage
            {
                Destination = user.Phone1.Replace("-", string.Empty),
                Body = text + "  " + url
            };

            var result = new SmsService().SendAsync(user, message, logo);

            switch (result)
            {
                case "0":
                    //¡Mensaje Enviado!
                    type = "success";
                    return "100075";
                case "1":
                    //¡Llave API vacía!
                    type = "warning";
                    return "100078";
                case "2":
                    //¡Teléfono Vacío!
                    type = "warning";
                    return "100079";
                case "3":
                    //¡Texto Vacío!
                    type = "warning";
                    return "100080";
                case "4":
                    //¡Sin Saldo!
                    type = "warning";
                    return "100081";
                case "9":
                    //¡Texto muy largo!
                    type = "warning";
                    return "100082";
                case "10":
                    //¡Teléfono muy largo!
                    type = "warning";
                    return "100083";
                case "11":
                    //¡Telefono Incorrecto!
                    type = "warning";
                    return "100084";
                case "20":
                    //¡Texto Incorrecto!
                    type = "warning";
                    return "100085";
                case "30":
                    //¡IP no autorizada!
                    type = "warning";
                    return "100086";
                case "90":
                    //¡Error en API!
                    type = "error";
                    return "100087";
                case "99":
                    //¡Usuario no autorizado!
                    type = "warning";
                    return "100088";
                default:
                    //¡Resultado API SMS Desconocido!"
                    type = "warning";
                    return "100089";

            }
        }

        public static string SendSMS(string phone, string text, string url, string logo, out string type)
        {
            type = string.Empty;

            var message = new IdentityMessage
            {
                Destination = phone.Replace("-", string.Empty),
                Body = text + "  " + url
            };

            var result = new SmsService().SendPromotionAsync(message, logo);

            switch (result)
            {
                case "0":
                    //¡Mensaje Enviado!
                    type = "success";
                    return "100026";
                case "1":
                    //¡Llave API vacía!
                    type = "warning";
                    return "100078";
                case "2":
                    //¡Teléfono Vacío!
                    type = "warning";
                    return "100079";
                case "3":
                    //¡Texto Vacío!
                    type = "warning";
                    return "100080";
                case "4":
                    //¡Sin Saldo!
                    type = "warning";
                    return "100081";
                case "9":
                    //¡Texto muy largo!
                    type = "warning";
                    return "100082";
                case "10":
                    //¡Teléfono muy largo!
                    type = "warning";
                    return "100083";
                case "11":
                    //¡Telefono Incorrecto!
                    type = "warning";
                    return "100084";
                case "20":
                    //¡Texto Incorrecto!
                    type = "warning";
                    return "100085";
                case "30":
                    //¡IP no autorizada!
                    type = "warning";
                    return "100086";
                case "90":
                    //¡Error en API!
                    type = "error";
                    return "100087";
                case "99":
                    //¡Usuario no autorizado!
                    type = "warning";
                    return "100088";
                default:
                    //¡Resultado API SMS Desconocido!"
                    type = "warning";
                    return "100089";

            }
        }
    }
}