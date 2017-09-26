using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.Helpers;
using Web;
using Web.Models;

namespace Test.Integration.JobControllerTests
{
    [TestClass]
    public class GetAllJobsShould
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync("/api/job").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ReturnEmptyList()
        {
            var response = _client.GetAsync("/api/job").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobViewModel>>(response);
            Assert.AreEqual(0, serializedContent.Count);
        }
    }
}