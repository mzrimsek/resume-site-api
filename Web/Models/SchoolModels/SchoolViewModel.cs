using Core.Interfaces;

namespace Web.Models.SchoolModels
{
    public class SchoolViewModel : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}