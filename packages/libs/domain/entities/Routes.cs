using System;

namespace Guestlogix.SkyRoutes.Domain
{
    public class Route
    {
        //Airline Id,Origin,Destination
        public string AirlineId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public float Distance { get; set; }
        public int Cost { get; set; }
        public Airport ConnectedNode { get; set; }
    }
}