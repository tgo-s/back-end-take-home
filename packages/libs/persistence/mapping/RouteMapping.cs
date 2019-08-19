using System;
using Guestlogix.SkyRoutes.Domain;
using TinyCsvParser.Mapping;

namespace Guestlogix.SkyRoutes.Persistence.Mapping
{
    public class RouteMapping : CsvMapping<Route>
    {
        public RouteMapping() : base()
        {
            MapProperty(0, a => a.AirlineId);
            MapProperty(1, a => a.Origin);
            MapProperty(2, a => a.Destination);
        }
    }
}