using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Test.Integration.Helpers
{
    public static class RequestHelper
    {
        public static HttpContent GetRequestContentFromObject(object value)
        {
            var valueJson = JsonConvert.SerializeObject(value);
            return new StringContent(valueJson, Encoding.UTF8, "application/json");
        }

        public static T GetObjectFromResponseContent<T>(HttpResponseMessage response)
        {
            var jsonString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}