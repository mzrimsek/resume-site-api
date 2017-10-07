using System.Net.Http;
using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Test.Integration.TestModels.LanguageModels;
using Test.Integration.TestModels.SchoolModels;
using Test.Integration.TestModels.SkillModels;

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
            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECTS}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response).Id;
        }

        public int GetIdFromNewSchool()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.SCHOOLS}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<SchoolViewModel>(response).Id;
        }

        public int GetIdFromNewLanguage()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.LANGUAGES}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<LanguageViewModel>(response).Id;
        }

        public int GetIdFromNewSkill(int languageId)
        {
            var model = TestObjectGetter.GetAddSkillViewModel(languageId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.SKILLS}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<SkillViewModel>(response).Id;
        }
    }
}