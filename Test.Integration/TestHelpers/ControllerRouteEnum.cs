namespace Test.Integration.TestHelpers
{
    public class ControllerRouteEnum
    {
        public static string JOB = new ControllerRouteEnum("job").Route;
        public static string SCHOOL = new ControllerRouteEnum("school").Route;
        public static string JOB_PROJECT = new ControllerRouteEnum("jobProject").Route;
        public static string LANGUAGE = new ControllerRouteEnum("language").Route;

        public string Route { get; private set; }
        public ControllerRouteEnum(string route)
        {
            Route = $"/api/{route}";
        }
    }
}