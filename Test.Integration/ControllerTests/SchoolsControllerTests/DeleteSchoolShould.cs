using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.SchoolsControllerTests
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
        public void ReturnStatusCodeNoContent_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var schoolId = _testObjectCreator.GetIdFromNewSchool();
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteSchool()
        {
            var schoolId = _testObjectCreator.GetIdFromNewSchool();

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}