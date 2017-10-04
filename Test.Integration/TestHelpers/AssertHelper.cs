using Test.Integration.TestModels.JobModels;
using Test.Integration.TestModels.JobProjectModels;
using Web.Models.LanguageModels;
using Web.Models.SchoolModels;

namespace Test.Integration.TestHelpers
{
    public static class AssertHelper
    {
        public static bool AreTestJobViewModelsEqual(TestAddJobViewModel expected, TestJobViewModel actual)
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

        public static bool AreJobProjectViewModelsEqual(TestAddJobProjectViewModel expected, TestJobProjectViewModel actual)
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
    }
}