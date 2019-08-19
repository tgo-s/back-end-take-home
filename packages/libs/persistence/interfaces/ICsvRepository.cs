using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Guestlogix.SkyRoutes.Domain;

namespace Guestlogix.SkyRoutes.Persistence.Interfaces
{
    public interface ICsvRepository
    {
        IEnumerable<Airline> ListAirlines();
        IEnumerable<Airline> ListAirlinesByParam(Func<Airline, bool> expression);
        Airline FindAirlineByParam(Func<Airline, bool> expression);
        IEnumerable<Airport> ListAirports();
        IEnumerable<Airport> ListAirportsByParam(Func<Airport, bool> expression);
        Airport FindAirportByParam(Func<Airport, bool> expression);
        IEnumerable<Route> ListRoutes();
        IEnumerable<Route> ListRoutesByParam(Func<Route, bool> expression);
        Route FindRouteByParam(Func<Route, bool> expression);
    }
}