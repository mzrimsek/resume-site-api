using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.JobProjectModels;

namespace Test.Integration.ControllerTests.JobProjectsControllerTests
{
    [TestClass]
    public class GetJobProjectsForJobShould
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
        public void ReturnStatusCodeNotFound_WhenGivenInvalidJobId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidJobId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/{jobId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenJobHasNoJobProjects()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();

            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/{jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);

            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneJobProject_WhenOneJobProjectIsCreatedForJob()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);

            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/{jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);

            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(jobProjectId);
        }
    }
}