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
    public class AddJobProjectShould
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
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var jobProjectPostReponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.Created, jobProjectPostReponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidJobId()
        {
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(0);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenValidModel_WithInvalidJobId()
        {
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var jobProjectPostReponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostReponse);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(jobProjectModel, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var jobProjectPostReponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostReponse).Id;
            var jobProjectGetReponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectGetReponse);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(jobProjectModel, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}