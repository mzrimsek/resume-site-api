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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = TestObjectGetter.GetAddJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Jobs, requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response).Id;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectGetter.GetAddJobViewModel(null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Jobs, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Jobs, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);
            _jobId = serializedContent.Id;

            var isCorrectViewModel = AssertHelper.AreTestJobViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Jobs, requestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response).Id;
            response = _client.GetAsync($"{ControllerRouteEnum.Jobs}/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestJobViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}