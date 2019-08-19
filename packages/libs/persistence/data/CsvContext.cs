using Guestlogix.SkyRoutes.Persistence.Mapping;
using Guestlogix.SkyRoutes.Domain;
using TinyCsvParser;
using System.Text;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Guestlogix.SkyRoutes.Persistence.Data
{
    public class CsvContext : IDisposable
    {
        private bool disposed = false;

        private readonly IConfiguration _settings;

        public CsvContext(IConfiguration settings)
        {
            _settings = settings;
            LoadCsvData();

        }
        public IEnumerable<Airline> Airlines { get; private set; }
        public IEnumerable<Airport> Airports { get; private set; }
        public IEnumerable<Route> Routes { get; private set; }

        private void LoadCsvData()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            var airlineParser = new CsvParser<Airline>(csvParserOptions, new AirlineMapping());
            this.Airlines = airlineParser.ReadFromFile(@_settings["AppSettings:AirlineData"], Encoding.UTF8).Select(x => x.Result).ToList();

            var airportParser = new CsvParser<Airport>(csvParserOptions, new AirportMapping());
            this.Airports = airportParser.ReadFromFile(@_settings["AppSettings:AirportData"], Encoding.UTF8).Select(x => x.Result).ToList();

            var routesParser = new CsvParser<Route>(csvParserOptions, new RouteMapping());
            this.Routes = routesParser.ReadFromFile(@_settings["AppSettings:RouteData"], Encoding.UTF8).Select(x => x.Result).ToList();

            foreach (var item in Airports)
            {
                item.Routes = Routes.Where(r => r.Origin.Trim().ToUpper().Equals(item.IATA.ToUpper().Trim()));
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //
            }

            disposed = true;
        }
    }
}