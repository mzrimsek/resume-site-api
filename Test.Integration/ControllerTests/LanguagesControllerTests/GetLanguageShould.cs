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
    public class GetLanguageShould
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Languages}/{_languageId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.Languages}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var response = _client.GetAsync($"{ControllerRouteEnum.Languages}/{_languageId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync(ControllerRouteEnum.Languages, requestContent).Result;
            _languageId = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response).Id;

            response = _client.GetAsync($"{ControllerRouteEnum.Languages}/{_languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreLanguageViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}