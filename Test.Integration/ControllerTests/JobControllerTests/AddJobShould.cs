using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.JobModels;

namespace Test.Integration.ControllerTests.JobControllerTests
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.JOB}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response).Id;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectGetter.GetAddUpdateJobViewModel(null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);
            _jobId = serializedContent.Id;

            var isCorrectViewModel = AssertHelper.AreJobViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var postResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB}/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(getResponse);

            var isCorrectViewModel = AssertHelper.AreJobViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}