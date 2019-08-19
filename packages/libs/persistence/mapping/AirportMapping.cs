using System;
using Guestlogix.SkyRoutes.Domain;
using TinyCsvParser.Mapping;

namespace Guestlogix.SkyRoutes.Persistence.Mapping
{
    public class AirportMapping : CsvMapping<Airport>
    {
        public AirportMapping() : base()
        {
            MapProperty(0, a => a.Name);
            MapProperty(1, a => a.City);
            MapProperty(2, a => a.Country);
            MapProperty(3, a => a.IATA);
            MapProperty(4, a => a.Latitude);
            MapProperty(5, a => a.Longitude);
        }
    }
}