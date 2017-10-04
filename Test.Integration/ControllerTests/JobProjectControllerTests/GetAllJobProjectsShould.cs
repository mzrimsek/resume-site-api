using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.JobProjectModels;

namespace Test.Integration.ControllerTests.JobProjectControllerTests
{
    [TestClass]
    public class GetAllJobProjectsShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _jobId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
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
        public void ReturnEmptyList_WhenNoJobProjectsAreCreated()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<TestJobProjectViewModel>>(response);
            Assert.AreEqual(0, serializedContent.Count);
        }

        [TestMethod]
        public void ReturnOneJobProject_WhenOneJobProjectIsCreated()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);

            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<TestJobProjectViewModel>>(response);

            Assert.AreEqual(1, serializedContent.Count);
            Assert.AreEqual(jobProjectId, serializedContent[0].Id);
        }
    }
}