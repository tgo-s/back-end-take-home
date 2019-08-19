using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Guestlogix.SkyRoutes.Api;
using Guestlogix.SkyRoutes.Tests.Api.Interfaces;
using Guestlogix.SkyRoutes.Tests.Api.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Guestlogix.SkyRoutes.Tests.Api.Base
{
    public abstract class ApiTestBase<T> where T : class
    {
        protected HttpClient _client;
        protected string _endpoint = "";
        CustomWebApiFactory<Startup> _factory;
        internal string _codigoValue = "";

        public virtual void Init()
        {
            _factory = new CustomWebApiFactory<Startup>();
        }

        public virtual void Cleanup()
        {
            _factory.Dispose();
        }

        public virtual void Setup()
        {
            _client = _factory.CreateClient();
        }

        public async Task<T1> CallEndpoint<T1>(string url) where T1 : ITestResult<T>
        {
            var response = await _client.GetAsync(url);
            T1 result = await EndpointProcessResponse<T1>(response);
            return result;
        }

        public async Task<object> CallEndpoint(string url)
        {
            var response = await _client.GetAsync(url);
            var result = EndpointProcessResponse(response);
            return result;
        }

        private static async Task<T> EndpointProcessResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(stringResponse);
            return result;
        }
        private static async Task<T1> EndpointProcessResponse<T1>(HttpResponseMessage response) where T1 : ITestResult<T>
        {
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T1>(stringResponse);
            return result;
        }

        public object GetRouteValueFromObj(Object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine(@"[DEBUG] Object: {0}
                                                Prop: {1} = {2}", t.Name, prop.Name, prop.GetValue(obj));
                if ((prop.Name == "Result") && (prop.GetValue(obj) != null))
                    return prop.GetValue(obj);
            }

            return null;
        }

    }
}