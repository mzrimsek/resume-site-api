using Core.Interfaces;

namespace Web.Models.JobModels
{
    public class JobViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}