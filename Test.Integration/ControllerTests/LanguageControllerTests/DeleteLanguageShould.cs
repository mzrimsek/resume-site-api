using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.LanguageControllerTests
{
    [TestClass]
    public class DeleteLanguageShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.LANGUAGE}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var languageId = _testObjectCreator.GetIdFromNewLanguage();
            var response = _client.DeleteAsync($"{ControllerRouteEnum.LANGUAGE}/{languageId}").Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void DeleteLanguage()
        {
            var languageId = _testObjectCreator.GetIdFromNewLanguage();

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.LANGUAGE}/{languageId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.LANGUAGE}/{languageId}").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}