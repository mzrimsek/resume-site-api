using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.LanguagesControllerTests
{
    [TestClass]
    public class DeleteLanguageShould
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
        public void ReturnStatusCodeNoContent_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Languages}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Languages}/{languageId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteLanguage()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Languages}/{languageId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Languages}/{languageId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void DeleteSkillsForLanguage()
        {
            var languageId = _testObjectCreator.GetIdForNewLanguage();
            var skillId = _testObjectCreator.GetIdForNewSkill(languageId);

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Languages}/{languageId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Skills}/{skillId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}