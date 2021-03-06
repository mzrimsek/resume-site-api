﻿using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.SocialMediaLinksControllerTests
{
    [TestClass]
    public class DeleteSocialMediaLinkShould
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
            var response = _client.DeleteAsync($"{ControllerRouteEnum.SocialMediaLinks}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            var response = _client.DeleteAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteSocialMediaLink()
        {
            var socialMediaLinkId = _testObjectCreator.GetIdForNewSocialMediaLink();
            
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.SocialMediaLinks}/{socialMediaLinkId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}