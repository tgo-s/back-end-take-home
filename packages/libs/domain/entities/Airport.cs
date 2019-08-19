using System;
using System.Collections.Generic;

namespace Guestlogix.SkyRoutes.Domain
{
    public class Airport
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<Route> Routes { get; set; }
        public int Score { get; set; }
        public int? MinCostToStart { get; set; }
        public Airport ConnectedNode { get; set; }
        public bool Visited { get; set; }
        public Airport NearestToStart { get; set; }
        public List<Airport> Connections { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return IATA.ToLower().Trim().Equals(((Airport)obj).IATA.Trim().ToLower());
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, IATA);
        }
    }
}