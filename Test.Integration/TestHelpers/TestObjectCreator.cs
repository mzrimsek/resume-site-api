using System.Net.Http;
using Web.Models.JobModels;
using Web.Models.JobProjectModels;

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
            var model = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<JobViewModel>(response).Id;
        }

        public int GetIdFromNewJobProject(int jobId)
        {
            var model = TestObjectGetter.GetAddUpdateJobProjectViewModel(jobId);
            var requestContent = RequestHelper.GetRequestContentFromObject(model);
            var response = _client.PostAsync($"{ControllerRouteEnum.JOB_PROJECT}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<JobProjectViewModel>(response).Id;
        }
    }
}