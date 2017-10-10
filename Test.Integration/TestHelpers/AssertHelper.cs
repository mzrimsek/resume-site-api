using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Test.Integration.TestModels.LanguageModels;
using Test.Integration.TestModels.SchoolModels;
using Test.Integration.TestModels.SkillModels;
using Test.Integration.TestModels.SocialMediaLinkModels;

namespace Test.Integration.TestHelpers
{
    public static class AssertHelper
    {
        public static bool AreTestJobViewModelsEqual(AddJobViewModel expected, JobViewModel actual)
        {
            return expected.Name == actual.Name
                && expected.City == actual.City
                && expected.State == actual.State
                && expected.Title == actual.Title
                && expected.StartDate == actual.StartDate
                && expected.EndDate == actual.EndDate;
        }

        public static bool AreSchoolViewModelsEqual(AddSchoolViewModel expected, SchoolViewModel actual)
        {
            return expected.Name == actual.Name
                && expected.City == actual.City
                && expected.State == actual.State
                && expected.Major == actual.Major
                && expected.Degree == actual.Degree
                && expected.StartDate == actual.StartDate
                && expected.EndDate == actual.EndDate;
        }

        public static bool AreJobProjectViewModelsEqual(AddJobProjectViewModel expected, JobProjectViewModel actual)
        {
            return expected.JobId == actual.JobId
                && expected.Name == actual.Name
                && expected.Description == actual.Description;
        }

        public static bool AreLanguageViewModelsEqual(AddLanguageViewModel expected, LanguageViewModel actual)
        {
            return expected.Name == actual.Name
                && expected.Rating == actual.Rating;
        }

        public static bool AreSkillViewModelsEqual(AddSkillViewModel expected, SkillViewModel actual)
        {
            return expected.LanguageId == actual.LanguageId
                && expected.Name == actual.Name
                && expected.Rating == actual.Rating;
        }

        public static bool AreTestSocialMediaLinkViewModelsEqual(AddSocialMediaLinkViewModel expected, SocialMediaLinkViewModel actual)
        {
            return expected.Name == actual.Name
                && expected.Url == actual.Url;
        }
    }
}