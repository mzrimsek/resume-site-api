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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, null);
            var putRequestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidJobId()
        {
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(0, "A different project");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenValidModel_WithInvalidJobId()
        {
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId + 1, "A different project");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidIdAndValidModel()
        {
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnUpdatedViewModel()
        {
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", putRequestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(putResponse);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(jobProjectModel, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;

            var jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId);
            requestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);
            var jobProjectPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(jobProjectPostResponse).Id;

            jobProjectModel = TestObjectGetter.GetAddUpdateJobProjectViewModel(_jobId, "A different project");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(jobProjectModel);

            var _ = _client.PutAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}", putRequestContent).Result;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.JOB_PROJECT}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(getResponse);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(jobProjectModel, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}