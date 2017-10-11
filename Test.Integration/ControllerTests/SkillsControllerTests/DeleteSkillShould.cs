using System.Net;
using System.Net.Http;
using FluentAssertions;
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Languages}/{_languageId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Skills}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            _languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(_languageId);

            var response = _client.DeleteAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteSkill()
        {
            _languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(_languageId);

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}