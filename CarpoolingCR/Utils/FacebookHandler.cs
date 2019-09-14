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
        //private const string FB_PAGE_ID = "387202725206585";
        //private const string FB_ACCESS_TOKEN = "EAAI1cyWWu3MBAMCvFFGp0O1iV6ZCryZBxSukyzMZCmhzHjEjRCTOjouVnDZBXcRXibw5bZCEfn96gOtpz2f5pgZA8DdSVlIbXdspEzkIZBQVA2VPMZCYxVRzivNtjvNM7Bdfwe0BetoRoDqvg3UVHXZBXOW8KsD6v8z3818EQ02QZBToyRIBj8cVlB";
        //private const string FB_BASE_ADDRESS = "https://graph.facebook.com/";

        public static async Task<string> PublishFacebookPost(string from, string to, string route, DateTime date, string currencyChar, decimal price, int availableSpaces, string callback)
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
    }
}