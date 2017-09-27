namespace Test.Integration.ControllerTests
{
    public class ControllerRouteEnum
    {
        public static string JOB = new ControllerRouteEnum("job").Route;
        public static string SCHOOL = new ControllerRouteEnum("school").Route;

        public string Route { get; private set; }
        public ControllerRouteEnum(string route)
        {
            Route = $"/api/{route}";
        }
    }
}