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
    public class UpdateJobProjectShould
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
            _jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidJobId()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(0, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenValidModel_WithInvalidJobId()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId + 1, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidIdAndValidModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ReturnUpdatedViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}