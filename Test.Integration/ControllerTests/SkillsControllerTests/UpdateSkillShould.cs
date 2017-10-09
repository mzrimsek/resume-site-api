using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SkillModels;

namespace Test.Integration.ControllerTests.SkillsControllerTests
{
    [TestClass]
    public class UpdateSkillShould
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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetUpdateSkillViewModel(1, _languageId, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, _languageId, null, 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/{skillId}", requestContent).Result;
            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, _languageId, "A different skill", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/{skillId}", requestContent).Result;
            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidLanguageId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, 0, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/{skillId}", requestContent).Result;
            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidModel_WithInvalidLanguageId()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, _languageId + 1, "A different skill", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/{skillId}", requestContent).Result;
            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, _languageId, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/{skillId}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var skillId = _testObjectCreator.GetIdFromNewSkill(_languageId);
            var model = TestObjectGetter.GetUpdateSkillViewModel(skillId, _languageId, "A different skill", 2);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.SKILLS}/{skillId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.SKILLS}/{skillId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SkillViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreSkillViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}