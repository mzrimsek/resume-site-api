using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.Helpers;
using Web.Models.JobModels;

namespace Test.Integration.ControllerTests.JobControllerTests
{
    [TestClass]
    public class UpdateJobShould
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
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync("/api/job/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", postRequestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            model = TestObjectCreator.GetAddUpdateJobViewModel(null);
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var putResponse = _client.PutAsync($"/api/job/{_jobId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidIdAndValidModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", postRequestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var putResponse = _client.PutAsync($"/api/job/{_jobId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnUpdatedViewModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", postRequestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            model = TestObjectCreator.GetAddUpdateJobViewModel("A Different Company");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var putResponse = _client.PutAsync($"/api/job/{_jobId}", putRequestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(putResponse);

            Assert.AreEqual(model.Name, serializedContent.Name);
            Assert.AreEqual(model.City, serializedContent.City);
            Assert.AreEqual(model.State, serializedContent.State);
            Assert.AreEqual(model.Title, serializedContent.Title);
            Assert.AreEqual(model.StartDate, serializedContent.StartDate);
            Assert.AreEqual(model.EndDate, serializedContent.EndDate);
        }
    }
}