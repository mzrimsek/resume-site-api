using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;

namespace Test.Integration.ControllerTests.JobsControllerTests
{
    [TestClass]
    public class DeleteJobShould
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
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var response = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/{jobId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteJob()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/{jobId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Jobs}/{jobId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void DeleteJobProjectsForJob()
        {
            var jobId = _testObjectCreator.GetIdForNewJob();
            var jobProjectId = _testObjectCreator.GetIdForNewJobProject(jobId);

            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Jobs}/{jobId}").Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.JobProjects}/{jobProjectId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}