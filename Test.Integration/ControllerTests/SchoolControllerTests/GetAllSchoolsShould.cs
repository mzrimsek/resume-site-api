using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolControllerTests
{
    [TestClass]
    public class GetAllSchoolsShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _schoolId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
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
            _schoolId = _testObjectCreator.GetIdFromNewSchool();

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SchoolViewModel>>(getResponse);

            Assert.AreEqual(1, serializedContent.Count);
            Assert.AreEqual(_schoolId, serializedContent[0].Id);
        }
    }
}