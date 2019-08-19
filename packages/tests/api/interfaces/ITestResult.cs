using System.Collections.Generic;

namespace Guestlogix.SkyRoutes.Tests.Api.Interfaces
{
    public interface ITestResult<T> where T : class
    {
        string Route { get; set; } 
    }
}