using System;

namespace Guestlogix.SkyRoutes.Application.Exceptions
{
    public class InvalidSkyRouteException : Exception
    {
        public InvalidSkyRouteException(string message)
              : base(message)
        {
        }
    }

}