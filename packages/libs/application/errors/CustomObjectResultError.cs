using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guestlogix.SkyRoutes.Application.Errors
{
    public class CustomObjectResult : ObjectResult
    {
        public CustomObjectResult(object value, int? statusCode) : base(value)
        {
            if(statusCode.HasValue) StatusCode = statusCode;
        }

        public CustomObjectResult() : this(null, null)
        {

        }
    }
}
