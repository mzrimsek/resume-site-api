using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Web.Models.LanguageModels;
using Web.Models.SchoolModels;

namespace Test.Integration.TestHelpers
{
    public static class TestObjectGetter
    {
        public static TestAddJobViewModel GetAddJobViewModel(string name)
        {
            return new TestAddJobViewModel()
            {
                Name = name,
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
        }

        public static TestAddJobViewModel GetAddJobViewModel()
        {
            return GetAddJobViewModel("Some Company");
        }

        public static TestUpdateJobViewModel GetUpdateJobViewModel(int id, string name)
        {
            var viewModel = GetAddJobViewModel(name);
            return new TestUpdateJobViewModel()
            {
                Id = id,
                Name = name,
                City = viewModel.City,
                State = viewModel.State,
                Title = viewModel.Title,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };
        }

        public static AddSchoolViewModel GetAddSchoolViewModel(string name)
        {
            return new AddSchoolViewModel()
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

        public static AddSchoolViewModel GetAddSchoolViewModel()
        {
            return GetAddSchoolViewModel("Kent State University");
        }

        public static UpdateSchoolViewModel GetUpdateSchoolViewModel(int id, string name)
        {
            var viewModel = GetAddSchoolViewModel(name);
            return new UpdateSchoolViewModel()
            {
                Id = id,
                Name = name,
                City = viewModel.City,
                State = viewModel.State,
                Major = viewModel.Major,
                Degree = viewModel.Degree,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };
        }

        public static TestAddJobProjectViewModel GetAddJobProjectViewModel(int jobId, string name)
        {
            return new TestAddJobProjectViewModel()
            {
                JobId = jobId,
                Name = name,
                Description = "Some project description"
            };
        }

        public static TestAddJobProjectViewModel GetAddJobProjectViewModel(int jobId)
        {
            return GetAddJobProjectViewModel(jobId, "Some project name");
        }

        public static TestUpdateJobProjectViewModel GetUpdateJobProjectViewModel(int id, int jobId, string name)
        {
            var viewModel = GetAddJobProjectViewModel(jobId, name);
            return new TestUpdateJobProjectViewModel()
            {
                Id = id,
                JobId = viewModel.JobId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }

        public static AddLanguageViewModel GetAddLanguageViewModel(string name, int rating)
        {
            return new AddLanguageViewModel()
            {
                Name = name,
                Rating = rating
            };
        }

        public static AddLanguageViewModel GetAddLanguageViewModel()
        {
            return GetAddLanguageViewModel("C#", 3);
        }

        public static UpdateLanguageViewModel GetUpdateLanguageViewModel(int id, string name, int rating)
        {
            return new UpdateLanguageViewModel()
            {
                Id = id,
                Name = name,
                Rating = rating
            };
        }
    }
}