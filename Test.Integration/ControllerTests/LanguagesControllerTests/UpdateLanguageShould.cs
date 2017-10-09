using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.LanguageModels;

namespace Test.Integration.ControllerTests.LanguagesControllerTests
{
    [TestClass]
    public class UpdateLanguageShould
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var model = TestObjectGetter.GetUpdateLanguageViewModel(1, "A different language", 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.LANGUAGES}/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(_languageId, null, 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(_languageId, "A different language", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(_languageId + 1, "A different language", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(_languageId, "A different language", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(_languageId, "A different language", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.LANGUAGES}/{_languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreLanguageViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}