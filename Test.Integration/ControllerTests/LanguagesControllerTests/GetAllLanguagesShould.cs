using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.LanguageModels;

namespace Test.Integration.ControllerTests.LanguagesControllerTests
{
    [TestClass]
    public class GetAllLanguagesShould
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
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.LANGUAGES}").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoLanguagesAreCreated()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.LANGUAGES}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<LanguageViewModel>>(response);
            Assert.AreEqual(0, serializedContent.Count);
        }

        [TestMethod]
        public void ReturnOneJob_WhenOneLanguageIsCreated()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();

            var response = _client.GetAsync($"{ControllerRouteEnum.LANGUAGES}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<LanguageViewModel>>(response);

            Assert.AreEqual(1, serializedContent.Count);
            Assert.AreEqual(_languageId, serializedContent[0].Id);
        }
    }
}