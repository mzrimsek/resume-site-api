using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.SkillsControllerTests
{
    [TestClass]
    public class AddSkillShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _languageId;

        [TestInitialize]
        public void Setup()
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
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            _languageId = _testObjectCreator.GetIdFromNewLanguage();
            var model = TestObjectGetter.GetAddSkillViewModel(_languageId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync($"{ControllerRouteEnum.SKILLS}", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidName()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidRating()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel_WithInvalidLanguageId()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidModel_WithInvalidLanguageId()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            Assert.Fail();
        }
    }
}