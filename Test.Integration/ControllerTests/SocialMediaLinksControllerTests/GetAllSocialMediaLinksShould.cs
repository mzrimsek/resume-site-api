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
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoSocialMediaLinksAreCreated()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SocialMediaLinkViewModel>>(response);
            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneSocialMediaLink_WhenOneSocialMediaLinkIsCreated()
        {
            _socialMediaLinkId = _testObjectCreator.GetIdFromNewSocialMediaLink();
            
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SocialMediaLinkViewModel>>(response);
            
            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(_socialMediaLinkId);
        }
    }
}