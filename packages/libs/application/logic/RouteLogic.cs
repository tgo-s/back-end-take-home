using System;
using System.Collections.Generic;
using System.Linq;
using Guestlogix.SkyRoutes.Domain;
using Guestlogix.SkyRoutes.Persistence.Interfaces;


namespace Guestlogix.SkyRoutes.Application.Logic
{
    public class RouteLogic
    {
        private readonly ICsvRepository _repository;
        public RouteLogic(ICsvRepository repository)
        {
            _repository = repository;
        }

        // A shortest route is defined as the route with the fewest connections.
        // If there are mulitple routes with the same number of connections, you may choose any of them.
        public IEnumerable<Airport> FindLeastConnectionsFlight(string origin, string destination)
        {

            var airports = LoadAirports().ToList();

            Airport originAirport = airports.Single(a => a.IATA.Trim().ToLower().Equals(origin.Trim().ToLower()));
            Airport destinationAirport = airports.Single(a => a.IATA.Trim().ToLower().Equals(destination.Trim().ToLower()));
            List<Airport> visited = new List<Airport>();
            List<Airport> connections = new List<Airport>();
            
            connections.Add(originAirport);

            RecursiveSearch(originAirport, destinationAirport, airports, visited, connections);

            return connections;
        }

        private void RecursiveSearch(Airport origin, Airport destination, List<Airport> airports, List<Airport> visited, List<Airport> connections)
        {
            if (!origin.Routes.Any(r => r.Destination.Equals(destination.IATA)))
            {
                visited.Add(origin);

                foreach (var item in origin.Routes)
                {
                    var newOrigin = airports.Where(a => !visited.Any(v => v.Equals(a))).SingleOrDefault(a => a.IATA.Equals(item.Destination));

                    if(newOrigin == null)
                        continue;

                    connections.Add(newOrigin);

                    RecursiveSearch(newOrigin, destination, airports, visited, connections);

                    if(connections.Any(c => c.Equals(destination)))
                    {
                        break;
                    }
                }
            }
            else
            {
                connections.Add(destination);
                return;
            }
        }

        private IEnumerable<Airport> LoadAirports()
        {
            try
            {
                var airports = _repository.ListAirports().ToList();
                return airports;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}