using Guestlogix.SkyRoutes.Tests.Api.Base;
using Guestlogix.SkyRoutes.Tests.Api.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Guestlogix.SkyRoutes.Tests.Api
{
    [TestFixture]
    public class ApiCausaIntervencaoControllerShould : ApiTestBase<ApiResultModel>
    {
        public ApiCausaIntervencaoControllerShould()
        {
            base._endpoint = "/api/v1/sky-routes/shortest";
        }

        [OneTimeSetUp]
        public override void Init() => base.Init();
        [SetUp]
        public override void Setup() => base.Setup();
        [OneTimeTearDown]
        public override void Cleanup() => base.Cleanup();

        [Test]
        public async Task ReturnShortestPath()
        {
            await ExecuteRouteTest("YYZ", "JFK", "YYZ => JFK");
        }

        [Test]
        public async Task ReturnShortestPathYVR()
        {
            await ExecuteRouteTest("YYZ", "YVR", "YYZ => JFK => LAX => YVR");
        }

        [Test]
        public async Task ReturnShortestPathNoRoute()
        {
            await ExecuteRouteTest("YYZ", "ORD", "No route");
        }

        [Test]
        public async Task ReturnShortestPathInvalidOrigin()
        {
            await ExecoutErrorRouteTest("XXX", "YYZ", "Invalid Origin");
        }

        [Test]
        public async Task ReturnShortestPathInvalidDestination()
        {
            await ExecoutErrorRouteTest("YYZ", "XXX", "Invalid Destination");
        }

        private async Task ExecoutErrorRouteTest(string origin, string destination, string expected)
        {
            _endpoint = string.Format("{0}?origin={1}&destination={2}", _endpoint, origin, destination);
            var result = await CallEndpoint(_endpoint);

            // Expected not null
            Assert.IsNotNull(result);

            // Expected
            var value = (ApiErrorModel)GetRouteValueFromObj(result);

            // Value returned correctly
            Assert.IsNotNull(value);

            // 
            Assert.IsTrue(value.message.Equals(expected));
        }

        private async Task ExecuteRouteTest(string origin, string destination, string expected)
        {
            _endpoint = string.Format("{0}?origin={1}&destination={2}", _endpoint, origin, destination);
            var result = await CallEndpoint(_endpoint);

            // Expected not null
            Assert.IsNotNull(result);

            // Expected
            var value = (ApiResultModel)GetRouteValueFromObj(result);

            // Value returned correctly
            Assert.IsNotNull(value);

            // 
            Assert.IsTrue(value.route.Equals(expected));
        }

    }

}