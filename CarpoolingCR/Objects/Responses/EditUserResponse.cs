using CarpoolingCR.Models;
using System.Web.Mvc;

namespace CarpoolingCR.Objects.Responses
{
    public class EditUserResponse
    {
        public ApplicationUser User { get; set; }
        public SelectList Countries { get; set; }
    }
}