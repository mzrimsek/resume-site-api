using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Test.Integration.TestModels.LanguageModels;
using Test.Integration.TestModels.SchoolModels;
using Test.Integration.TestModels.SkillModels;
using Test.Integration.TestModels.SocialMediaLinkModels;

namespace Test.Integration.TestHelpers
{
    public static class TestObjectGetter
    {
        public static AddJobViewModel GetAddJobViewModel(string name = "Some Company")
        {
            return new AddJobViewModel
            {
                Name = name,
                City = "San Francisco",
                State = "CA",
                Title = "Developer",
                StartDate = "1/1/2017",
                EndDate = "7/1/2017"
            };
        }

        public static UpdateJobViewModel GetUpdateJobViewModel(int id, string name)
        {
            var viewModel = GetAddJobViewModel(name);
            return new UpdateJobViewModel
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

        public static AddSchoolViewModel GetAddSchoolViewModel(string name = "Kent State University")
        {
            return new AddSchoolViewModel
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

        public static UpdateSchoolViewModel GetUpdateSchoolViewModel(int id, string name)
        {
            var viewModel = GetAddSchoolViewModel(name);
            return new UpdateSchoolViewModel
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

        public static AddJobProjectViewModel GetAddJobProjectViewModel(int jobId, string name = "Some project name")
        {
            return new AddJobProjectViewModel
            {
                JobId = jobId,
                Name = name,
                Description = "Some project description"
            };
        }

        public static UpdateJobProjectViewModel GetUpdateJobProjectViewModel(int id, int jobId, string name)
        {
            var viewModel = GetAddJobProjectViewModel(jobId, name);
            return new UpdateJobProjectViewModel
            {
                Id = id,
                JobId = viewModel.JobId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }

        public static AddLanguageViewModel GetAddLanguageViewModel(string name = "C#", int rating = 3)
        {
            return new AddLanguageViewModel
            {
                Name = name,
                Rating = rating
            };
        }

        public static UpdateLanguageViewModel GetUpdateLanguageViewModel(int id, string name, int rating)
        {
            return new UpdateLanguageViewModel
            {
                Id = id,
                Name = name,
                Rating = rating
            };
        }

        public static AddSkillViewModel GetAddSkillViewModel(int languageId, string name = "MVC", int rating = 3)
        {
            return new AddSkillViewModel
            {
                LanguageId = languageId,
                Name = name,
                Rating = rating
            };
        }

        public static UpdateSkillViewModel GetUpdateSkillViewModel(int id, int languageId, string name, int rating)
        {
            return new UpdateSkillViewModel
            {
                Id = id,
                LanguageId = languageId,
                Name = name,
                Rating = rating
            };
        }

        public static AddSocialMediaLinkViewModel GetAddSocialMediaLinkViewModel(string name = "GitHub")
        {
            return new AddSocialMediaLinkViewModel
            {
                Name = name,
                Url = "https://github.com/mzrimsek"
            };
        }

        public static UpdateSocialMediaLinkViewModel GetUpdateSocialMediaLinkViewModel(int id, string name)
        {
            return new UpdateSocialMediaLinkViewModel
            {
                Id = id,
                Name = name,
                Url = "https://github.com/mzrimsek"
            };
        }
    }
}