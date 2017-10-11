using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Test.Integration.TestHelpers
{
    public class TestSetupHelper
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _testClient;

        public TestSetupHelper()
        {
            _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _testClient = _testServer.CreateClient();
        }
        
        public HttpClient GetTestClient()
        {
            return _testClient;
        }

        public void DisposeTestServerAndClient()
        {
            _testClient.Dispose();
            _testServer.Dispose();
        }
    }
}