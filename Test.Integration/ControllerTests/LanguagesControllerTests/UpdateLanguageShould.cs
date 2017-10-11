using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.LanguageModels;

namespace Test.Integration.ControllerTests.LanguagesControllerTests
{
    [TestClass]
    public class UpdateLanguageShould
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
            var model = TestObjectGetter.GetUpdateLanguageViewModel(1, "A different language", 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Languages}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(languageId, null, 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Languages}/{languageId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(languageId, "A different language", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Languages}/{languageId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(languageId + 1, "A different language", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Languages}/{languageId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(languageId, "A different language", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Languages}/{languageId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetUpdateLanguageViewModel(languageId, "A different language", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.Languages}/{languageId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Languages}/{languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestLanguageViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}