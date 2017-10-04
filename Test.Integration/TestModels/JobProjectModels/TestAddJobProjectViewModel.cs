using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.JobProjectModels
{
    public class TestAddJobProjectViewModel
    {
        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "Invalid Job ID")]
        public int JobId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}