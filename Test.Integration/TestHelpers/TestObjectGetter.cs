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

        public static UpdateJobViewModel GetUpdateJobViewModel(int id, string name)
        {
            var viewModel = GetAddJobViewModel(name);
            return new UpdateJobViewModel()
            {
                Id = id,
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Title = viewModel.Title,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };
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

        public static AddJobProjectViewModel GetAddJobProjectViewModel(int jobId, string name)
        {
            return new AddJobProjectViewModel()
            {
                JobId = jobId,
                Name = name,
                Description = "Some project description"
            };
        }

        public static AddJobProjectViewModel GetAddJobProjectViewModel(int jobId)
        {
            return GetAddJobProjectViewModel(jobId, "Some project name");
        }

        public static UpdateJobProjectViewModel GetUpdateJobProjectViewModel(int id, int jobId, string name)
        {
            var viewModel = GetAddJobProjectViewModel(jobId, name);
            return new UpdateJobProjectViewModel()
            {
                Id = id,
                JobId = viewModel.JobId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
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