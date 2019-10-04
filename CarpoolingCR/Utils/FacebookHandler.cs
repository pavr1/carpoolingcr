using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CarpoolingCR.Utils
{
    public class FacebookHandler
    {
        public static async Task<string> PublishTripCreation(string from, string to, string route, DateTime date, string currencyChar, decimal price, int availableSpaces, string callback)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/html"));

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["FacebookBaseAddress"]);

                var message = "Se ha creado un nuevo viaje de " + from + " a " + to + ", ruta " + route +
                    ".\nFecha: " + Common.ConvertToLocalTime(date).ToString(WebConfigurationManager.AppSettings["DateTimeFormat"]) +
                    "\nEspacios Disponibles: " + availableSpaces +
                    "\nCuota: " + currencyChar + price.ToString("N2") +
                    "\n\n¡Visita www.buscoridecr.com para reservar!";

                var parametters = new Dictionary<string, string>
                {
                    { "access_token", WebConfigurationManager.AppSettings["FacebookAccessToken"] },
                    { "message", message }
                };
                var encodedContent = new FormUrlEncodedContent(parametters);

                var result = await httpClient.PostAsync($"{WebConfigurationManager.AppSettings["FacebookPageId"]}/feed", encodedContent);
                var msg = result.EnsureSuccessStatusCode();
                return await msg.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> PublishNotification(string from, string to, DateTime date, string timeDetail)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/html"));

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["FacebookBaseAddress"]);

                var message = "Hay usuarios que están en búsqueda de rides desde " + from + " hasta " + to + " para el " + Common.ConvertToLocalTime(date).ToString(WebConfigurationManager.AppSettings["DateTime"]) + " " + timeDetail +
                    ".\n\n¡Aprovecha y crea tu viaje! Nos encargaremos de notificar a los usuarios interesados." +
                    "\n\n¡Visita www.buscoridecr.com!" +
                    "\n\n¡Hagamos Ride!";

                var parametters = new Dictionary<string, string>
                {
                    { "access_token", WebConfigurationManager.AppSettings["FacebookAccessToken"] },
                    { "message", message }
                };

                var encodedContent = new FormUrlEncodedContent(parametters);

                var result = await httpClient.PostAsync($"{WebConfigurationManager.AppSettings["FacebookPageId"]}/feed", encodedContent);
                var msg = result.EnsureSuccessStatusCode();
                return await msg.Content.ReadAsStringAsync();
            }
        }
    }
}