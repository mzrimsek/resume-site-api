using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.SkillsControllerTests
{
    [TestClass]
    public class DeleteSkillShould
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
        public void ReturnStatusCodeNoContent_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.SKILLS}/1").Result;
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);

            var response = _client.DeleteAsync($"{ControllerRouteEnum.SKILLS}/{skillId}").Result;

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void DeleteSkill()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SKILLS}/{skillId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.SKILLS}/{skillId}").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}