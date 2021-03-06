using Core.Interfaces;

namespace Test.Integration.TestModels.JobProjectModels
{
    public class JobProjectViewModel : IHasId
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}