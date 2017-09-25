using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Test.Integration.Helpers
{
    public static class RequestHelper
    {
        public static HttpContent GetContentFromObject(object value)
        {
            var valueJson = JsonConvert.SerializeObject(value);
            return new StringContent(valueJson, Encoding.UTF8, "application/json");
        }
    }
}