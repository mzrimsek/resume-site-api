using System.Net.Http;
using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Web.Models.LanguageModels;
using Web.Models.SchoolModels;

namespace Test.Integration.TestHelpers
{
    public class TestObjectCreator
    {
        private HttpClient _client;
        public TestObjectCreator(HttpClient client)
        {
            _client = client;
        }

        public int GetIdForNewJob()
        {
            var model = TestObjectGetter.GetAddJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<TestJobViewModel>(response).Id;
        }

        public int GetIdFromNewJobProject(int jobId)
        {
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<TestJobProjectViewModel>(response).Id;
        }

        public int GetIdFromNewSchool()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.SCHOOL}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response).Id;
        }

        public int GetIdFromNewLanguage()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGE}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response).Id;
        }
    }
}