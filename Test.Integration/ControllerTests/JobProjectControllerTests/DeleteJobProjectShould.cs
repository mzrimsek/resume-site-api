using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.JobModels;
using Web.Models.JobProjectModels;

namespace Test.Integration.ControllerTests.JobProjectControllerTests
{
    [TestClass]
    public class DeleteJobProjectShould
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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.JOB_PROJECT}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostReponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostReponse).Id;

            var deleteReponse = _client.DeleteAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;

            Assert.AreEqual(HttpStatusCode.NoContent, deleteReponse.StatusCode);
        }

        [TestMethod]
        public void DeleteJobProject()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostReponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostReponse).Id;

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}