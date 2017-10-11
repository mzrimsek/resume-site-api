using Core.Interfaces;

namespace Test.Integration.TestModels.ProjectModels
{
    public class ProjectViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
    }
}