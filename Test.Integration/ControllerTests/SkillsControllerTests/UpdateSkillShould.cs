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
    public class UpdateSkillShould
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
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetUpdateSkillViewModel(1, languageId, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Skills}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, languageId, null, 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Skills}/{skillId}", requestContent).Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, languageId, "A different skill", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Skills}/{skillId}", requestContent).Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidLanguageId()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, 0, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Skills}/{skillId}", requestContent).Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidModel_WithInvalidLanguageId()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, languageId + 1, "A different skill", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Skills}/{skillId}", requestContent).Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, languageId, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Skills}/{skillId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, languageId, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.Skills}/{skillId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SkillViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSkillViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}