using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolControllerTests
{
    [TestClass]
    public class DeleteSchoolShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;

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
            var response = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var schoolId = _testObjectCreator.GetIdFromNewSchool();
            var deleteReponse = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/{schoolId}").Result;
            Assert.AreEqual(HttpStatusCode.NoContent, deleteReponse.StatusCode);
        }

        [TestMethod]
        public void DeleteSchool()
        {
            var schoolId = _testObjectCreator.GetIdFromNewSchool();

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/{schoolId}").Result;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}/{schoolId}").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}