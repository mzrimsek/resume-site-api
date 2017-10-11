using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.ProjectModels;

namespace Test.Integration.ControllerTests.ProjectsControllerTests
{
    [TestClass]
    public class UpdateProjectShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _projectId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var model = TestObjectGetter.GetUpdateProjectViewModel(1, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            _projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(_projectId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/{_projectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            _projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(_projectId + 1, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/{_projectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            _projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(_projectId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/{_projectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(_projectId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.Projects}/{_projectId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Projects}/{_projectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<ProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}