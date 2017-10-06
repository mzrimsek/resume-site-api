using System.Net.Http;
using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Test.Integration.TestModels.LanguageModels;
using Test.Integration.TestModels.SchoolModels;

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
            var response = _client.PostAsync($"{ControllerRouteEnum.JOBS}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<JobViewModel>(response).Id;
        }

        public int GetIdFromNewJobProject(int jobId)
        {
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response).Id;
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