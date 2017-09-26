using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.JobModels;

namespace Test.Integration.ControllerTests.JobControllerTests
{
    [TestClass]
    public class GetJobShould
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
            var _ = _client.DeleteAsync($"/api/job/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.GetAsync("/api/job/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;

            var getResponse = _client.GetAsync($"/api/job/{_jobId}").Result;

            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;

            var getResponse = _client.GetAsync($"/api/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(getResponse);

            Assert.AreEqual(model.Name, serializedContent.Name);
            Assert.AreEqual(model.City, serializedContent.City);
            Assert.AreEqual(model.State, serializedContent.State);
            Assert.AreEqual(model.Title, serializedContent.Title);
            Assert.AreEqual(model.StartDate, serializedContent.StartDate);
            Assert.AreEqual(model.EndDate, serializedContent.EndDate);
        }
    }
}