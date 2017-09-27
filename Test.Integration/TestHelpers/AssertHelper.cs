using Web.Models.JobModels;

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
    }
}