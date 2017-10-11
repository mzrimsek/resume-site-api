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
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(1, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(socialMediaLinkId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(socialMediaLinkId + 1, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(socialMediaLinkId, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            var model = TestObjectGetter.GetUpdateSocialMediaLinkViewModel(socialMediaLinkId, "A different website");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SocialMediaLinkViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSocialMediaLinkViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}