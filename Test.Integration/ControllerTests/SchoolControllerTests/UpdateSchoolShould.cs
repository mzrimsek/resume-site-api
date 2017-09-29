using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using Test.Integration.TestHelpers;
using Web.Models.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolControllerTests
{
    [TestClass]
    public class UpdateSchoolShould
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
            var model = TestObjectGetter.GetAddUpdateSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.SCHOOL}/1", requestContent).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectGetter.GetAddUpdateSchoolViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", postRequestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            model = TestObjectGetter.GetAddUpdateSchoolViewModel(null);
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidIdAndValidModel()
        {
            var model = TestObjectGetter.GetAddUpdateSchoolViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", postRequestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            model = TestObjectGetter.GetAddUpdateSchoolViewModel("A Different School");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}", putRequestContent).Result;

            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
        }

        [TestMethod]
        public void ReturnUpdatedViewModel()
        {
            var model = TestObjectGetter.GetAddUpdateSchoolViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", postRequestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            model = TestObjectGetter.GetAddUpdateSchoolViewModel("A Different School");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var putResponse = _client.PutAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}", putRequestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(putResponse);

            var isCorrectViewModel = AssertHelper.AreSchoolViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            var model = TestObjectGetter.GetAddUpdateSchoolViewModel();
            var postRequestContent = RequestHelper.GetRequestContentFromObject(model);
            var postResponse = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", postRequestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(postResponse).Id;

            model = TestObjectGetter.GetAddUpdateSchoolViewModel("A Different School");
            var putRequestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}", putRequestContent).Result;
            var getResponse = _client.GetAsync($"{ControllerRouteEnum.SCHOOL}/{_schoolId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(getResponse);

            var isCorrectViewModel = AssertHelper.AreSchoolViewModelsEqual(model, serializedContent);
            Assert.IsTrue(isCorrectViewModel);
        }
    }
}