using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.JobModels
{
    public class TestAddJobViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}