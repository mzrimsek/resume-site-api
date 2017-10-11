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
    public class AddSocialMediaLinkShould
    {
        private TestServer _server;
        private HttpClient _client;
        private int _socialMediaLinkId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = TestObjectGetter.GetAddSocialMediaLinkViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.SocialMediaLinks, requestContent).Result;
            _socialMediaLinkId = RequestHelper.GetObjectFromResponseContent<SocialMediaLinkViewModel>(response).Id;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectGetter.GetAddSocialMediaLinkViewModel(null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.SocialMediaLinks, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddSocialMediaLinkViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.SocialMediaLinks, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SocialMediaLinkViewModel>(response);
            _socialMediaLinkId = serializedContent.Id;

            var isCorrectViewModel = AssertHelper.AreTestSocialMediaLinkViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
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