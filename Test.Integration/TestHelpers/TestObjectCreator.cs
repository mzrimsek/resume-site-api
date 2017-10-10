using System.Net.Http;
using Core.Interfaces;
using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Test.Integration.TestModels.LanguageModels;
using Test.Integration.TestModels.SchoolModels;
using Test.Integration.TestModels.SkillModels;

namespace Test.Integration.TestHelpers
{
    public class TestObjectCreator
    {
        private readonly HttpClient _client;
        public TestObjectCreator(HttpClient client)
        {
            _client = client;
        }

        public int GetIdForNewJob()
        {
            var model = TestObjectGetter.GetAddJobViewModel();
            return GetIdFromNewTestObject<JobViewModel>(model, ControllerRouteEnum.Jobs);
        }

        public int GetIdFromNewJobProject(int jobId)
        {
            var model = TestObjectGetter.GetAddJobProjectViewModel(jobId);
            return GetIdFromNewTestObject<JobProjectViewModel>(model, ControllerRouteEnum.JobProjects);
        }

        public int GetIdFromNewSchool()
        {
            var model = TestObjectGetter.GetAddSchoolViewModel();
            return GetIdFromNewTestObject<SchoolViewModel>(model, ControllerRouteEnum.Schools);
        }

        public int GetIdFromNewLanguage()
        {
            var model = TestObjectGetter.GetAddLanguageViewModel();
            return GetIdFromNewTestObject<LanguageViewModel>(model, ControllerRouteEnum.Languages);
        }

        public int GetIdFromNewSkill(int languageId)
        {
            var model = TestObjectGetter.GetAddSkillViewModel(languageId);
            return GetIdFromNewTestObject<LanguageViewModel>(model, ControllerRouteEnum.Skills);
        }

        private int GetIdFromNewTestObject<T>(object model, string route) where T : IHasId
        {
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{route}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<T>(response).Id;
        }
    }
}