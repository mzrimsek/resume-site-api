using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace Test.Integration.TestHelpers
{
    public class TestSetupHelper
    {
        public (TestServer server, HttpClient client) GetTestServerAndClient()
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var testClient = testServer.CreateClient();

            return (testServer, testClient);
        }
    }
}