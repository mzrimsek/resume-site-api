using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.JobModels;
using Web.Models.JobProjectModels;

namespace Test.Integration.ControllerTests.JobProjectControllerTests
{
    [TestClass]
    public class GetAllJobProjectsShould
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ReturnEmptyList()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);
            Assert.AreEqual(0, serializedContent.Count);
        }

        [TestMethod]
        public void ReturnOneJobProject_WhenOneJobProjectIsCreated()
        {
            var jobModel = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectCreator.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var _ = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(getResponse);

            Assert.AreEqual(1, serializedContent.Count);
        }
    }
}