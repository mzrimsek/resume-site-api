using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.JobProjectModels;

namespace Test.Integration.ControllerTests.JobProjectsControllerTests
{
    [TestClass]
    public class UpdateJobProjectShould
    {
        private TestSetupHelper _testSetupHelper;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;

        [TestInitialize]
        public void SetUp()
        {
            _testSetupHelper = new TestSetupHelper();
            _client = _testSetupHelper.GetTestClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            _testSetupHelper.DisposeTestServerAndClient();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(1, jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(jobProjectId, jobId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidJobId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(jobProjectId, 0, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidModel_WithInvalidJobId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(jobProjectId, jobId + 1, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(jobProjectId + 1, jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(jobProjectId, jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);
            var model = TestObjectGetter.GetUpdateJobProjectViewModel(jobProjectId, jobId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestJobProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}