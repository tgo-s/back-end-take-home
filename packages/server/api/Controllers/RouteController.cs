using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Guestlogix.SkyRoutes.Api.Base;
using Guestlogix.SkyRoutes.Application.Exceptions;
using Guestlogix.SkyRoutes.Application.Logic;
using Guestlogix.SkyRoutes.Domain;
using Guestlogix.SkyRoutes.Persistence.Data;
using Guestlogix.SkyRoutes.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Guestlogix.SkyRoutes.Api
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sky-routes")]
    [ApiController]
    public class RouteController : BaseRouteController
    {
        private readonly ICsvRepository _repository;
        public RouteController(ICsvRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet("shortest")]
        public IActionResult Get(string origin, string destination)
        {
            try
            {
                string msg = string.Empty;

                if (!IsValidRoute(origin, destination, out msg))
                    throw new InvalidSkyRouteException(msg);

                RouteLogic routeLogic = new RouteLogic(_repository);

                var route = routeLogic.FindLeastConnectionsFlight(origin, destination);

                return Ok(BuildOutputResult(route));
            }
            catch (System.Exception e)
            {
                if (e is InvalidSkyRouteException)
                    return CustomClientError(e, (int)HttpStatusCode.BadRequest);
                else
                    return InternalServerError(e);
            }
        }

        private bool IsValidRoute(string origin, string destination, out string msg)
        {
            msg = string.Empty;
            bool isValid = false;

            if (!string.IsNullOrEmpty(origin) && !string.IsNullOrEmpty(destination))
            {
                var airporOrigin = _repository.FindAirportByParam(a => a.IATA.ToLower().Trim().Equals(origin.ToLower().Trim()));
                var airportDestination = _repository.FindAirportByParam(a => a.IATA.Trim().ToLower().Equals(destination.ToLower().Trim()));

                if (airporOrigin == null || airportDestination == null)
                    msg = string.Format("Invalid {0}", airporOrigin == null ? "Origin" : "Destination");
                else
                    isValid = true;
            }

            return isValid;
        }

        private object BuildOutputResult(IEnumerable<Airport> route)
        {   
            var output = string.Empty;
            if (route != null && route.Count() > 0)
            {
                output = string.Join(" => ", route.Select(s => s.IATA));
            }
            else{
                output = "No route";
            }
            return new
            {
                route = output
            };
        }
    }
}
