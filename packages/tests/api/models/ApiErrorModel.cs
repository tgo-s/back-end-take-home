using System;

namespace Guestlogix.SkyRoutes.Tests.Api.Models
{
    public class ApiErrorModel
    {
        public int status { get; set; }
        public string method { get; set; }
        public string path { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public DateTime ts { get; set; }
    }
}