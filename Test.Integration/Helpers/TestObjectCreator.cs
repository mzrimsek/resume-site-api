using Web.Models;

namespace Test.Integration.Helpers
{
    public static class TestObjectCreator
    {
        public static AddUpdateJobViewModel GetAddUpdateJobViewModel()
        {
            return new AddUpdateJobViewModel()
            {
                Name = "Some Company",
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
        }
    }
}