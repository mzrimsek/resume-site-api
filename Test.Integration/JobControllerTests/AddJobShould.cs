using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.Helpers;
using Web.Models.JobModels;

namespace Test.Integration.JobControllerTests
{
    [TestClass]
    public class AddJobShould
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
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync("/api/job", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response).Id;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel(null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync("/api/job", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync("/api/job", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);
            _jobId = serializedContent.Id;

            Assert.AreEqual(model.Name, serializedContent.Name);
            Assert.AreEqual(model.City, serializedContent.City);
            Assert.AreEqual(model.State, serializedContent.State);
            Assert.AreEqual(model.Title, serializedContent.Title);
            Assert.AreEqual(model.StartDate, serializedContent.StartDate);
            Assert.AreEqual(model.EndDate, serializedContent.EndDate);
        }
    }
}