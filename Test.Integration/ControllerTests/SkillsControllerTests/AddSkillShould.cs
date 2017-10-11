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
    public class AddSkillShould
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
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(languageId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(languageId, null, 1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(languageId, "MVC", 4);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidLanguageId()
        {
            var model = TestObjectGetter.GetAddSkillViewModel(0);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidModel_WithInvalidLanguageId()
        {
            var model = TestObjectGetter.GetAddSkillViewModel(1);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(languageId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Skills, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SkillViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSkillViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
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