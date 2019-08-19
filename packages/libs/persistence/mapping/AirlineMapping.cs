using System;
using Guestlogix.SkyRoutes.Domain;
using TinyCsvParser.Mapping;

namespace Guestlogix.SkyRoutes.Persistence.Mapping
{
    public class AirlineMapping : CsvMapping<Airline>
    {
        public AirlineMapping() : base()
        {
            MapProperty(0, a => a.Name);
            MapProperty(1, a => a.TwoDigitCode);
            MapProperty(2, a => a.ThreeDigitCode);
            MapProperty(3, a => a.Country);
        }
    }
}