using System.Net;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestHelpers;
using Test.Integration.TestModels.SchoolModels;

namespace Test.Integration.ControllerTests.SchoolsControllerTests
{
    [TestClass]
    public class UpdateSchoolShould
    {
        private TestServer _server;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;
        private int _schoolId;

        [TestInitialize]
        public void SetUp()
        {
            (_server, _client) = new TestSetupHelper().GetTestServerAndClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            var _ = _client.DeleteAsync($"{ControllerRouteEnum.Schools}/{_schoolId}").Result;
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public void ReturnStatusCodeNotFound_WhenGivenInvalidId()
        {
            var model = TestObjectGetter.GetUpdateSchoolViewModel(1, "A Different School");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Schools}/1", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            _schoolId = _testObjectCreator.GetIdForNewSchool();
            var model = TestObjectGetter.GetUpdateSchoolViewModel(_schoolId, null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Schools}/{_schoolId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenValidIdAndValidModel_WithNonMatchingId()
        {
            _schoolId = _testObjectCreator.GetIdForNewSchool();
            var model = TestObjectGetter.GetUpdateSchoolViewModel(_schoolId + 1, "A Different School");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Schools}/{_schoolId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnStatusCodeNoContent_WhenGivenValidIdAndValidModel()
        {
            _schoolId = _testObjectCreator.GetIdForNewSchool();
            var model = TestObjectGetter.GetUpdateSchoolViewModel(_schoolId, "A Different School");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PutAsync($"{ControllerRouteEnum.Schools}/{_schoolId}", requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void SaveUpdatedViewModel()
        {
            _schoolId = _testObjectCreator.GetIdForNewSchool();
            var model = TestObjectGetter.GetUpdateSchoolViewModel(_schoolId, "A Different School");
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var _ = _client.PutAsync($"{ControllerRouteEnum.Schools}/{_schoolId}", requestContent).Result;
            var response = _client.GetAsync($"{ControllerRouteEnum.Schools}/{_schoolId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSchoolViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}