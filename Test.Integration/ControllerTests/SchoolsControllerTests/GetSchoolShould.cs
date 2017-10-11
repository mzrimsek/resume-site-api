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
    public class GetSchoolShould
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
            var response = _client.GetAsync($"{ControllerRouteEnum.Schools}/1").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void ReturnStatusCodeOk_WhenGivenValidId()
        {
            _schoolId = _testObjectCreator.GetIdFromNewSchool();
            var response = _client.GetAsync($"{ControllerRouteEnum.Schools}/{_schoolId}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync(ControllerRouteEnum.Schools, requestContent).Result;
            _schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response).Id;

            response = _client.GetAsync($"{ControllerRouteEnum.Schools}/{_schoolId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreSchoolViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}