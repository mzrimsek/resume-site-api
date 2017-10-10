using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SkillModels;

namespace Test.Integration.ControllerTests.SkillsControllerTests
{
    [TestClass]
    public class GetAllSkillsShould
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
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoSkillsAreCreated()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SkillViewModel>>(response);
            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneSkill_WhenOneSkillIsCreated()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);

            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SkillViewModel>>(response);

            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(skillId);
        }
    }
}