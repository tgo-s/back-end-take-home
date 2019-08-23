using System;

namespace Guestlogix.SkyRoutes.Domain
{
    public class Route
    {
        public Route()
        {
            Cost = 0;
        }
        //Airline Id,Origin,Destination
        public string AirlineId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Cost { get; set; }
    }
}