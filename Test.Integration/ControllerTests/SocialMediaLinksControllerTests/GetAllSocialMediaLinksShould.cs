using System.Collections.Generic;
using System.Linq;
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
    public class GetAllSocialMediaLinksShould
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
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync(ControllerRouteEnum.SocialMediaLinks).Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoSocialMediaLinksAreCreated()
        {
            var response = _client.GetAsync(ControllerRouteEnum.SocialMediaLinks).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SocialMediaLinkViewModel>>(response);
            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneSocialMediaLink_WhenOneSocialMediaLinkIsCreated()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            
            var response = _client.GetAsync(ControllerRouteEnum.SocialMediaLinks).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SocialMediaLinkViewModel>>(response);
            
            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(socialMediaLinkId);
        }
    }
}