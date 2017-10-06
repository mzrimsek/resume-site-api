using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.JobProjectModels;

namespace Test.Integration.ControllerTests.JobProjectsControllerTests
{
    [TestClass]
    public class GetJobProjectsForJobShould
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOBS}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidJobId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECTS}/job/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidJobId()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECTS}/job/{_jobId}").Result;
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenJobHasNoJobProjects()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECTS}/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);

            Assert.AreEqual(0, serializedContent.Count);
        }

        [TestMethod]
        public void ReturnOneJobProject_WhenOneJobProjectIsCreatedForJob()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);

            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECTS}/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);

            Assert.AreEqual(1, serializedContent.Count);
            Assert.AreEqual(jobProjectId, serializedContent[0].Id);
        }
    }
}