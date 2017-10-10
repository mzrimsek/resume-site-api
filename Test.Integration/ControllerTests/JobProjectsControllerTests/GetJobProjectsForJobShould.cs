using System.Collections.Generic;
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
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _jobId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/{_jobId}").Result;
            _client.Dispose();
            _server.Dispose();
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
            _jobId = _testObjectCreator.GetIdForNewJob();
            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/{_jobId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenJobHasNoJobProjects()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);

            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneJobProject_WhenOneJobProjectIsCreatedForJob()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdFromNewJobProject(_jobId);

            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/job/{_jobId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobProjectViewModel>>(response);

            serializedContent.Should().HaveCount(1);
            serializedContent[0].Id.Should().Be(jobProjectId);
        }
    }
}