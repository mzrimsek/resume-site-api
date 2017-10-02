using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.LanguageModels;

namespace Test.Integration.ControllerTests.LanguageControllerTests
{
    [TestClass]
    public class AddLanguageShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _languageId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.LANGUAGE}/{_languageId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = TestObjectGetter.GetAddUpdateLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGE}", requestContent).Result;
            _languageId = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response).Id;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var model = TestObjectGetter.GetAddUpdateLanguageViewModel(null, 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGE}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            var model = TestObjectGetter.GetAddUpdateLanguageViewModel("Java", 0);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGE}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddUpdateLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGE}", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);
            _languageId = serializedContent.Id;

            var isCorrectViewModel = AssertHelper.AreLanguageViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddUpdateLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGE}", requestContent).Result;
            _languageId = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response).Id;
            response = _client.GetAsync($"{ControllerRouteEnum.LANGUAGE}/{_languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreLanguageViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}