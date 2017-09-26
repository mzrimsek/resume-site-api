using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.Helpers;
using Web.Models.JobModels;

namespace Test.Integration.JobControllerTests
{
    [TestClass]
    public class GetAllJobsShould
    {
        private TestServer _server;
        private HttpClient _client;
        private int _jobId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            _client.DeleteAsync($"/api/job/{_jobId}");
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

        [TestMethod]
        public void ReturnOneJob_WhenOneJobIsCreated()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var postResponse = _client.PostAsync("/api/job", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            var getResponse = _client.GetAsync("/api/job").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobViewModel>>(getResponse);

            Assert.AreEqual(1, serializedContent.Count);
        }
    }
}