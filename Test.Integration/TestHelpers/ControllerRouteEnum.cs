namespace Test.Integration.TestHelpers
{
    public class ControllerRouteEnum
    {
        public static string JOBS = new ControllerRouteEnum("jobs").Route;
        public static string SCHOOLS = new ControllerRouteEnum("schools").Route;
        public static string JOB_PROJECTS = new ControllerRouteEnum("jobProjects").Route;
        public static string LANGUAGES = new ControllerRouteEnum("languages").Route;

        public string Route { get; private set; }
        public ControllerRouteEnum(string route)
        {
            Route = $"/api/{route}";
        }
    }
}