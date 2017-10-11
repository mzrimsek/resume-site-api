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
    public class GetSkillsForLanguageShould
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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidLanguageId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidLanguageId()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/{languageId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenLanguageHasNoSkills()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/{languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SkillViewModel>>(response);
            
            serializedContent.Should().BeEmpty();
        }
        
        [TestMethod]
        public void ReturnOneSkill_WhenOneSkillCreatedForLanguage()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/{languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SkillViewModel>>(response);

            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(skillId);
        }
    }
}