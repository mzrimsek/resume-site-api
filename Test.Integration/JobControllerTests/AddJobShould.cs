using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.Helpers;
using Web;
using Web.Models;

namespace Test.Integration.JobControllerTests
{
    [TestClass]
    public class AddJobShould
    {
        private TestServer _server;
        private HttpClient _client;
        private int _jobId;

        [TestInitialize]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            _client.DeleteAsync($"/api/job/${_jobId}");
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = new AddUpdateJobViewModel()
            {
                Name = "Some Company",
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
            var requestContent = RequestHelper.GetContentFromObject(model);

            var response = _client.PostAsync("/api/job", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);
            _jobId = serializedContent.Id;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = new AddUpdateJobViewModel()
            {
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
            var requestContent = RequestHelper.GetContentFromObject(model);

            var response = _client.PostAsync("/api/job", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrespondingViewModel()
        {
            var model = new AddUpdateJobViewModel()
            {
                Name = "Some Company",
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
            var requestContent = RequestHelper.GetContentFromObject(model);

            var response = _client.PostAsync("/api/job", requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<JobViewModel>(response);
            _jobId = serializedContent.Id;

            Assert.AreEqual(model.Name, serializedContent.Name);
            Assert.AreEqual(model.City, serializedContent.City);
            Assert.AreEqual(model.State, serializedContent.State);
            Assert.AreEqual(model.Title, serializedContent.Title);
            Assert.AreEqual(model.StartDate, serializedContent.StartDate);
            Assert.AreEqual(model.EndDate, serializedContent.EndDate);
        }
    }
}