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
            var model = TestObjectGetter.GetUpdateProjectViewModel(1, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(projectId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/{projectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            var projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(projectId + 1, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/{projectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            var projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(projectId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Projects}/{projectId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var projectId = _testObjectCreator.GetIdForNewProject();
            var model = TestObjectGetter.GetUpdateProjectViewModel(projectId, "A different project");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.Projects}/{projectId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Projects}/{projectId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<ProjectViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestProjectViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}