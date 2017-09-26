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
    public class UpdateJobShould
    {
        private TestServer _server;
        private HttpClient _client;
        private int _jobId;

        [TestInitialize]
        public void SetUp()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestCleanup]
        public void TearDown()
        {

            _client.DeleteAsync($"/api/job/{_jobId}");
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetContentFromObject(model);

            var response = _client.PutAsync("/api/job/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", postRequestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            model.Name = null;
            var putRequestContent = RequestHelper.GetContentFromObject(model);

            var putResponse = _client.PutAsync($"/api/job/{_jobId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidIdAndValidModel()
        {
            var model = TestObjectCreator.GetAddUpdateJobViewModel();
            var postRequestContent = RequestHelper.GetContentFromObject(model);
            var postResponse = _client.PostAsync("/api/job", postRequestContent).Result;
            _jobId = RequestHelper.GetObjectFromResponseContent<JobViewModel>(postResponse).Id;
            var putRequestContent = RequestHelper.GetContentFromObject(model);

            var putResponse = _client.PutAsync($"/api/job/{_jobId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnUpdatedViewModel()
        {
            Assert.Fail();
        }
    }
}