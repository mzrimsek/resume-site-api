using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.JobModels;

namespace Test.Integration.ControllerTests.JobsControllerTests
{
    [TestClass]
    public class GetAllJobsShould
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
        public void ReturnStatusCodeOk()
        {
            var response = _client.GetAsync(ControllerRouteEnum.Jobs).Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnEmptyList_WhenNoJobsAreCreated()
        {
            var response = _client.GetAsync(ControllerRouteEnum.Jobs).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobViewModel>>(response);
            serializedContent.Should().BeEmpty();
        }

        [TestMethod]
        public void ReturnOneJob_WhenOneJobIsCreated()
        {
            _jobId = _testObjectCreator.GetIdForNewJob();

            var response = _client.GetAsync(ControllerRouteEnum.Jobs).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<List<JobViewModel>>(response);

            serializedContent.Should().HaveCount(1);
            serializedContent.First().Id.Should().Be(_jobId);
        }
    }
}