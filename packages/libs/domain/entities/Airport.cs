using System;
using System.Collections.Generic;

namespace Guestlogix.SkyRoutes.Domain
{
    public class Airport
    {
        public Airport()
        {
            StartCost = int.MaxValue;
        }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<Route> Routes { get; set; }
        public Airport LastConnection { get; set; }
        public int Cost { get; set; }
        public int StartCost { get; set; }
        public bool Visited { get; set; }


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