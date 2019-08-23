using System;
using System.Collections.Generic;
using Guestlogix.SkyRoutes.Domain;

namespace Guestlogix.SkyRoutes.Application.Comparer
{
    public class RouteComparer : IEqualityComparer<Route>
    {
        public bool Equals(Route x, Route y)
        {
            if (x.Origin.Trim().ToUpper().Equals(y.Origin.ToUpper().Trim())
                && x.Destination.Trim().ToUpper().Equals(y.Destination.Trim().ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Route obj)
        {
            return HashCode.Combine(obj.Origin.Trim().ToUpper(), obj.Destination.Trim().ToUpper());
        }
    }
}