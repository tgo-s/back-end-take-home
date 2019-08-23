using System;
using System.Collections.Generic;
using System.Linq;
using Guestlogix.SkyRoutes.Application.Comparer;
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

            SearchShortestPath(originAirport, destinationAirport, airports);
            var shortestPath = new List<Airport>();
            shortestPath.Add(destinationAirport);
            BuildShortestPath(shortestPath, destinationAirport);
            shortestPath.Reverse();

            return shortestPath;
        }

        public void SearchShortestPath(Airport originAiport, Airport destinationAirport, List<Airport> airports)
        {
            originAiport.StartCost = 0;
            var queue = new List<Airport>();
            queue.Add(originAiport);
            do
            {
                queue = queue.OrderBy(x => x.StartCost).ToList();
                var airport = queue.First();
                queue.Remove(airport);
                foreach (var route in airport.Routes.Distinct(new RouteComparer()).OrderBy(x => x.Cost))
                {
                    route.Cost += 1;
                    var newOrigin = airports.SingleOrDefault(a => a.IATA.Equals(route.Destination));

                    if (newOrigin.Visited)
                        continue;

                    if (newOrigin.StartCost == 0 ||
                        airport.StartCost + route.Cost < newOrigin.StartCost)
                    {
                        newOrigin.StartCost = airport.StartCost + route.Cost;
                        newOrigin.LastConnection = airport;
                        if (!queue.Contains(newOrigin))
                            queue.Add(newOrigin);
                    }
                }
                airport.Visited = true;
                if (airport.Equals(destinationAirport))
                    return;
            } while (queue.Any());
        }
        private void BuildShortestPath(List<Airport> shortestPath, Airport airport)
        {
            if (airport.LastConnection == null)
                return;
            shortestPath.Add(airport.LastConnection);
            BuildShortestPath(shortestPath, airport.LastConnection);
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