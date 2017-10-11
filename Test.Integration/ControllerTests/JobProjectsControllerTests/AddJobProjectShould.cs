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
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId, null);
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
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestJobProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.JobProjects, requestContent).Result;
            var jobProjectId = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response).Id;
            response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestJobProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}