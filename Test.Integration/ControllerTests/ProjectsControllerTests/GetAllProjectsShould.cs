using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.ProjectModels;

namespace Test.Integration.ControllerTests.ProjectsControllerTests
{
    [TestClass]
    public class GetAllProjectsShould
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
            var response = _client.GetAsync(ControllerRouteEnum.Projects).Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [TestMethod]
        public void ReturnEmptyList_WhenNoProjectsAreCreated()
        {
            var response = _client.GetAsync(ControllerRouteEnum.Projects).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<ProjectViewModel>>(response);
            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneProject_WhenOneProjectIsCreated()
        {
            var projectId = _testObjectCreator.GetIdForNewProject();

            var response = _client.GetAsync(ControllerRouteEnum.Projects).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<ProjectViewModel>>(response);

            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(projectId);
        }
    }
}