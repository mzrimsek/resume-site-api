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
    public class AddLanguageShould
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
            var model = TestObjectGetter.GetAddLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Languages, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel(null, 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Languages, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel("Java", 0);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Languages, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Languages, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestLanguageViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Languages, requestContent).Result;
            var languageId = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response).Id;
            response = _client.GetAsync($"{ControllerRouteEnum.Languages}/{languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestLanguageViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}