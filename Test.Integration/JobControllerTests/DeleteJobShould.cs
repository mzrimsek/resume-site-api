using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.Helpers;

namespace Test.Integration.JobControllerTests
{
    [TestClass]
    public class DeleteJobShould
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync("/api/job/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}