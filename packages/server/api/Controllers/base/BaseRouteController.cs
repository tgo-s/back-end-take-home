using System;
using System.Net;
using Guestlogix.SkyRoutes.Application.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Guestlogix.SkyRoutes.Api.Base
{
    public class BaseRouteController : Controller
    {
        private readonly string GeneralErrorTitle = "Error";
        private readonly string CriticalErrorTitle = "Critical Error";
        protected CustomObjectResult InternalServerError()
        {
            string defaultMessage = "An unexpected error has occurred. Please contact the api support.";
            var error = BuildCustomError(defaultMessage, "0", (int)HttpStatusCode.InternalServerError, CriticalErrorTitle);
            return new CustomObjectResult(error, (int)HttpStatusCode.InternalServerError);
        }

        protected CustomObjectResult InternalServerError(Exception e)
        {
            var error = BuildCustomError(e.Message, e.HResult.ToString("X8"), (int)HttpStatusCode.InternalServerError, CriticalErrorTitle);
            return new CustomObjectResult(error, (int)HttpStatusCode.InternalServerError);
        }

        protected CustomObjectResult CustomClientError(Exception e, int statusCode)
        {
            var error = BuildCustomError(e.Message, e.HResult.ToString("X8"), statusCode, GeneralErrorTitle);
            return new CustomObjectResult(error, statusCode);
        }

        private object BuildCustomError(string message, string code, int statusCode, string title)
        {
            var errorMessage = new
            {
                status = statusCode,
                method = HttpContext.Request.Method,
                path = string.Format("{0}{1}", HttpContext.Request.PathBase, HttpContext.Request.Path),
                code = string.Format("0x{0}", code),
                title = title,
                message = message,
                ts = DateTime.Now.ToUniversalTime()
            };

            return errorMessage;
        }
    }
}