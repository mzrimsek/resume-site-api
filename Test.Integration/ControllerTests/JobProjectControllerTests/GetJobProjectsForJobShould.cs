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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidJobId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/job/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidJobId()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/job/{_jobId}").Result;
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenJobHasNoJobProjects()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(getResponse);

            Assert.AreEqual(0, serializedContent.Count);
        }

        [TestMethod]
        public void ReturnOneJobProject_WhenOneJobProjectIsCreatedForJob()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var _ = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(getResponse);

            Assert.AreEqual(1, serializedContent.Count);
        }
    }
}