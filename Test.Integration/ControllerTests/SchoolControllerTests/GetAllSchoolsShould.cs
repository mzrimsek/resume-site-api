using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolControllerTests
{
    [TestClass]
    public class GetAllSchoolsShould
    {
        private TestServer _server;
        private HttpClient _client;
        private int _schoolId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoSchoolsAreCreated()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SchoolViewModel>>(response);
            Assert.AreEqual(0, serializedContent.Count);
        }

        [TestMethod]
        public void ReturnOneSchool_WhenOneSchoolIsCreated()
        {
            var model = TestObjectGetter.GetAddUpdateSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", requestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SchoolViewModel>>(getResponse);

            Assert.AreEqual(1, serializedContent.Count);
        }
    }
}