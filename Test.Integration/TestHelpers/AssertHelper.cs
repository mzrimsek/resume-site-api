using Web.Models.JobModels;
using Web.Models.JobProjectModels;
using Web.Models.SchoolModels;

namespace Test.Integration.TestHelpers
{
    public static class AssertHelper
    {
        public static bool AreJobViewModelsEqual(AddUpdateJobViewModel expected, JobViewModel actual)
        {
            return expected.Name == actual.Name
                && expected.City == actual.City
                && expected.State == actual.State
                && expected.Title == actual.Title
                && expected.StartDate == actual.StartDate
                && expected.EndDate == actual.EndDate;
        }

        public static bool AreSchoolViewModelsEqual(AddUpdateSchoolViewModel expected, SchoolViewModel actual)
        {
            return expected.Name == actual.Name
                && expected.City == actual.City
                && expected.State == actual.State
                && expected.Major == actual.Major
                && expected.Degree == actual.Degree
                && expected.StartDate == actual.StartDate
                && expected.EndDate == actual.EndDate;
        }

        public static bool AreJobProjectViewModelsEqual(AddUpdateJobProjectViewModel expected, JobProjectViewModel actual)
        {
            return expected.JobId == actual.JobId
                && expected.Name == actual.Name
                && expected.Description == actual.Description;
        }
    }
}