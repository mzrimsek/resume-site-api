using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolControllerTests
{
    [TestClass]
    public class GetSchoolShould
    {
        private TestServer _server;
        private HttpClient _client;
        private int _schoolId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var response = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}/1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            var model = TestObjectCreator.GetAddUpdateSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", requestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}").Result;

            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectCreator.GetAddUpdateSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", requestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            var getResponse = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(getResponse);

            Assert.AreEqual(model.Name, serializedContent.Name);
            Assert.AreEqual(model.City, serializedContent.City);
            Assert.AreEqual(model.State, serializedContent.State);
            Assert.AreEqual(model.Major, serializedContent.Major);
            Assert.AreEqual(model.Degree, serializedContent.Degree);
            Assert.AreEqual(model.StartDate, serializedContent.StartDate);
            Assert.AreEqual(model.EndDate, serializedContent.EndDate);
        }
    }
}