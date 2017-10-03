using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.JobModels;
using Web.Mappers.JobMappers;

namespace Test.Integration.ControllerTests.JobControllerTests
{
    [TestClass]
    public class UpdateJobShould
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
            var addViewModel = TestObjectGetter.GetAddJobViewModel("A Different Company");
            var model = JobViewModelMapper.MapFrom(1, addViewModel);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB}/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var addViewModel = TestObjectGetter.GetAddJobViewModel(null);
            var model = JobViewModelMapper.MapFrom(_jobId, addViewModel);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB}/{_jobId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var addViewModel = TestObjectGetter.GetAddJobViewModel("A Different Company");
            var model = JobViewModelMapper.MapFrom(_jobId + 1, addViewModel);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB}/{_jobId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidIdAndValidModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var addViewModel = TestObjectGetter.GetAddJobViewModel("A Different Company");
            var model = JobViewModelMapper.MapFrom(_jobId, addViewModel);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB}/{_jobId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ReturnUpdatedViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var addViewModel = TestObjectGetter.GetAddJobViewModel("A Different Company");
            var model = JobViewModelMapper.MapFrom(_jobId, addViewModel);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.JOB}/{_jobId}", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreJobViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var addViewModel = TestObjectGetter.GetAddJobViewModel("A Different Company");
            var model = JobViewModelMapper.MapFrom(_jobId, addViewModel);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.JOB}/{_jobId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.JOB}/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreJobViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}