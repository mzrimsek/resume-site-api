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
    public class AddSchoolShould
    {
        private TestSetupHelper _testSetupHelper;
        private HttpClient _client;
        private TestObjectCreator _testObjectCreator;

        [TestInitialize]
        public void SetUp()
        {
            _testSetupHelper = new TestSetupHelper();
            _client = _testSetupHelper.GetTestClient();
            _testObjectCreator = new TestObjectCreator(_client);
        }

        [TestCleanup]
        public void TearDown()
        {
            _testSetupHelper.DisposeTestServerAndClient();
        }

        [TestMethod]
        public void ReturnStatusCodeCreated_WhenGivenValidModel()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Schools, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public void ReturnStatusCodeBadRequest_WhenGivenInvalidModel()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel(null);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Schools, requestContent).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Schools, requestContent).Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSchoolViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }

        [TestMethod]
        public void SaveCorrectViewModel()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);

            var response = _client.PostAsync(ControllerRouteEnum.Schools, requestContent).Result;
            var schoolId = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response).Id;
            response = _client.GetAsync($"{ControllerRouteEnum.Schools}/{schoolId}").Result;
            var serializedContent = RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response);

            var isCorrectViewModel = AssertHelper.AreTestSchoolViewModelsEqual(model, serializedContent);
            isCorrectViewModel.Should().BeTrue();
        }
    }
}