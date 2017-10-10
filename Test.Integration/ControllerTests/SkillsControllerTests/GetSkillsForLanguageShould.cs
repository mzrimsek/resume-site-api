using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SkillModels;

namespace Test.Integration.ControllerTests.SkillsControllerTests
{
    [TestClass]
    public class GetSkillsForLanguageShould
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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidLanguageId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidLanguageId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/{_languageId}").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);            
        }

        [TestMethod]
        public void ReturnEmptyList_WhenLanguageHasNoSkills()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/{_languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SkillViewModel>>(response);
            
            Assert.AreEqual(0, serializedContent.Count); 
        }
        
        [TestMethod]
        public void ReturnOneSkill_WhenOneSkillCreatedForLanguage()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/language/{_languageId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SkillViewModel>>(response);
            
            Assert.AreEqual(1, serializedContent.Count); 
            Assert.AreEqual(skillId, serializedContent[0].Id);
        }
    }
}