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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(_jobId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidJobId()
        {
            var model = TestObjectGetter.GetAddJobProjectViewModel(0);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidModel_WithInvalidJobId()
        {
            var model = TestObjectGetter.GetAddJobProjectViewModel(1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(_jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response).Id;
            response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreJobProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}