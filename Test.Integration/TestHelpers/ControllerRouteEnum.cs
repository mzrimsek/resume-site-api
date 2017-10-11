namespace Test.Integration.TestHelpers
{
    public class ControllerRouteEnum
    {
        public static readonly string Jobs = new ControllerRouteEnum("jobs").Route;
        public static readonly string Schools = new ControllerRouteEnum("schools").Route;
        public static readonly string JobProjects = new ControllerRouteEnum("jobProjects").Route;
        public static readonly string Languages = new ControllerRouteEnum("languages").Route;
        public static readonly string Skills = new ControllerRouteEnum("skills").Route;
        public static readonly string SocialMediaLinks = new ControllerRouteEnum("socialMediaLinks").Route;
        public static readonly string Projects = new ControllerRouteEnum("projects").Route;

        private string Route { get; set; }
        private ControllerRouteEnum(string route)
        {
            Route = $"/api/{route}";
        }
    }
}