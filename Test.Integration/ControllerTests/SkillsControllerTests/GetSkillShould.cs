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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);

            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(languageId);
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