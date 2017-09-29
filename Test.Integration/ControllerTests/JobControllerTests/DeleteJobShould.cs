using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.JobModels;
using Web.Models.JobProjectModels;

namespace Test.Integration.ControllerTests.JobControllerTests
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

        [TestCleanup]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", postRequestContent).Result;
            var jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;

            var deleteReponse = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/{jobId}").Result;

            Assert.AreEqual(HttpStatusCode.NoContent, deleteReponse.StatusCode);
        }

        [TestMethod]
        public void DeleteJob()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", postRequestContent).Result;
            var jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/{jobId}").Result;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB}/{jobId}").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [TestMethod]
        public void DeleteJobProjectsForJob()
        {
            var jobModel = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            var jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectCreator.GetAddUpdateJobProjectViewModel(jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/{jobId}").Result;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(getResponse);

            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}