using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolControllerTests
{
    [TestClass]
    public class DeleteSchoolShould
    {
        private TestServer _server;
        private HttpClient _client;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidId()
        {
            var model = TestObjectCreator.GetAddUpdateSchoolViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", postRequestContent).Result;
            var schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            var deleteReponse = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/{schoolId}").Result;

            Assert.AreEqual(HttpStatusCode.NoContent, deleteReponse.StatusCode);
        }

        [TestMethod]
        public void DeleteSchool()
        {
            var model = TestObjectCreator.GetAddUpdateSchoolViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync("/api/school", postRequestContent).Result;
            var schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            var _ = _client.DeleteAsync($"/api/school/{schoolId}").Result;
            var getResponse = _client.GetAsync($"/api/school/${schoolId}").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}