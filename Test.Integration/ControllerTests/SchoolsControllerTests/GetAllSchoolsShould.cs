using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolsControllerTests
{
    [TestClass]
    public class GetAllSchoolsShould
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
            var response = _client.GetAsync(ControllerRouteEnum.Schools).Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoSchoolsAreCreated()
        {
            var response = _client.GetAsync(ControllerRouteEnum.Schools).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SchoolViewModel>>(response);
            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneSchool_WhenOneSchoolIsCreated()
        {
            var schoolId = _testObjectCreator.GetIdForNewSchool();

            var getResponse = _client.GetAsync(ControllerRouteEnum.Schools).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<SchoolViewModel>>(getResponse);

            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(schoolId);
        }
    }
}