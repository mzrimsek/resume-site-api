using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SocialMediaLinkModels;

namespace Test.Integration.ControllerTests.SocialMediaLinksControllerTests
{
    [TestClass]
    public class GetSocialMediaLinkShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _socialMediaLinkId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            _socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddSocialMediaLinkViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync(ControllerRouteEnum.SocialMediaLinks, requestContent).Result;
            _socialMediaLinkId = RequestHelper.GetObjectFromResponseContent<SocialMediaLinkViewModel>(response).Id;

            response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SocialMediaLinkViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSocialMediaLinkViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}