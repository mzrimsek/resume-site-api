using System.Net.Http;
using Web.Models.JobModels;

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
            var jobModel = TestObjectGetter.GetAddUpdateJobViewModel();
            var requestContent = RequestHelper.GetRequestContentFromObject(jobModel);
            var jobPostResponse = _client.PostAsync($"{ControllerRouteEnum.JOB}", requestContent).Result;
            return RequestHelper.GetObjectFromResponseContent<JobViewModel>(jobPostResponse).Id;
        }
    }
}