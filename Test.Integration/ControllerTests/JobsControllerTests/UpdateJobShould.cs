using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.JobModels;

namespace Test.Integration.ControllerTests.JobsControllerTests
{
    [TestClass]
    public class UpdateJobShould
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
            var model = TestObjectGetter.GetUpdateJobViewModel(1, "A Different Company");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Jobs}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetUpdateJobViewModel(jobId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Jobs}/{jobId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetUpdateJobViewModel(jobId + 1, "A Different Company");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Jobs}/{jobId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetUpdateJobViewModel(jobId, "A Different Company");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Jobs}/{jobId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var model = TestObjectGetter.GetUpdateJobViewModel(jobId, "A Different Company");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.Jobs}/{jobId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Jobs}/{jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestJobViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}