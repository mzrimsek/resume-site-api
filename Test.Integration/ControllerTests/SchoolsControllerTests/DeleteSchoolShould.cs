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
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var schoolId = _testObjectCreator.GetIdForNewSchool();
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteSchool()
        {
            var schoolId = _testObjectCreator.GetIdForNewSchool();

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}