using Web.Models.JobModels;

namespace Test.Integration.Helpers
{
    public static class TestObjectCreator
    {
        public static AddUpdateJobViewModel GetAddUpdateJobViewModel()
        {
            return GetAddUpdateJobViewModel("Some Company");
        }

        public static AddUpdateJobViewModel GetAddUpdateJobViewModel(string name)
        {
            return new AddUpdateJobViewModel()
            {
                Name = name,
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
        }
    }
}