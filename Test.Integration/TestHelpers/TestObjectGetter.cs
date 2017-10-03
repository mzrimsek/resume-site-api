using Web.Models.JobModels;
using Web.Models.JobProjectModels;
using Web.Models.LanguageModels;
using Web.Models.SchoolModels;

namespace Test.Integration.TestHelpers
{
    public static class TestObjectGetter
    {
        public static AddJobViewModel GetAddJobViewModel(string name)
        {
            return new AddJobViewModel()
            {
                Name = name,
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
        }

        public static AddJobViewModel GetAddJobViewModel()
        {
            return GetAddJobViewModel("Some Company");
        }

        public static AddUpdateSchoolViewModel GetAddUpdateSchoolViewModel(string name)
        {
            return new AddUpdateSchoolViewModel()
            {
                Name = name,
                City = "Kent",
                State = "OH",
                Major = "Computer Science",
                Degree = "B.S.",
                StartDate = "9/1/2015",
                EndDate = "5/13/2017"
            };
        }

        public static AddUpdateSchoolViewModel GetAddUpdateSchoolViewModel()
        {
            return GetAddUpdateSchoolViewModel("Kent State University");
        }

        public static AddUpdateJobProjectViewModel GetAddUpdateJobProjectViewModel(int jobId, string name)
        {
            return new AddUpdateJobProjectViewModel()
            {
                JobId = jobId,
                Name = name,
                Description = "Some project description"
            };
        }

        public static AddUpdateJobProjectViewModel GetAddUpdateJobProjectViewModel(int jobId)
        {
            return GetAddUpdateJobProjectViewModel(jobId, "Some project name");
        }

        public static AddUpdateLanguageViewModel GetAddUpdateLanguageViewModel(string name, int rating)
        {
            return new AddUpdateLanguageViewModel()
            {
                Name = name,
                Rating = rating
            };
        }

        public static AddUpdateLanguageViewModel GetAddUpdateLanguageViewModel()
        {
            return GetAddUpdateLanguageViewModel("C#", 3);
        }
    }
}