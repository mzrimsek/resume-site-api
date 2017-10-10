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
    public class UpdateSocialMediaLinkShould
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
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(1, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            _socialMediaLinkId = _testObjectCreator.GetIdFromNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(_socialMediaLinkId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            _socialMediaLinkId = _testObjectCreator.GetIdFromNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(_socialMediaLinkId + 1, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            _socialMediaLinkId = _testObjectCreator.GetIdFromNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(_socialMediaLinkId, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _socialMediaLinkId = _testObjectCreator.GetIdFromNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(_socialMediaLinkId, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}/{_socialMediaLinkId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SocialMediaLinkViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSocialMediaLinkViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}