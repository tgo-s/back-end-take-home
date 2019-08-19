using Guestlogix.SkyRoutes.Persistence.Interfaces;
using Guestlogix.SkyRoutes.Persistence.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using Guestlogix.SkyRoutes.Domain;
using System.Collections.Generic;

namespace Guestlogix.SkyRoutes.Persistence.Repository
{
    public class CsvRepository : ICsvRepository
    {
        private readonly CsvContext _context;
        public CsvRepository(CsvContext context)
        {
            _context = context;
        }

        public Airline FindAirlineByParam(Func<Airline, bool> expression)
        {
            try
            {
                var resource = _context.Airlines.FirstOrDefault(expression);
                return resource;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public Airport FindAirportByParam(Func<Airport, bool> expression)
        {
            try
            {
                var resource = _context.Airports.FirstOrDefault(expression);

                if(resource != null)
                    resource.Routes = _context.Routes.Where(r => r.Origin.Trim().ToLower().Equals(resource.IATA.Trim().ToLower()));

                return resource;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public Route FindRouteByParam(Func<Route, bool> expression)
        {
            try
            {
                var resource = _context.Routes.FirstOrDefault(expression);
                return resource;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Airline> ListAirlines()
        {
            try
            {
                return _context.Airlines;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Airline> ListAirlinesByParam(Func<Airline, bool> expression)
        {
            try
            {
                return _context.Airlines.Where(expression);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Airport> ListAirports()
        {
            try
            {
                return _context.Airports;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Airport> ListAirportsByParam(Func<Airport, bool> expression)
        {
            try
            {
                return _context.Airports.Where(expression);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Route> ListRoutes()
        {
            try
            {
                return _context.Routes;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Route> ListRoutesByParam(Func<Route, bool> expression)
        {
            try
            {
                return _context.Routes.Where(expression);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}