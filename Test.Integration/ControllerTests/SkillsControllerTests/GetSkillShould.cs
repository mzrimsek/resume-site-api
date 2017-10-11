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
    public class GetSkillShould
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
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            _languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(_languageId);

            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            _languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(_languageId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;
            var skillId = RequestHelper.GetObjectFromResponseContent<SkillViewModel>(response).Id;

            response = _client.GetAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SkillViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSkillViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}