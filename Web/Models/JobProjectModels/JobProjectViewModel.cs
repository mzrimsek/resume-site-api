using System.ComponentModel.DataAnnotations;

namespace Web.Models.JobProjectModels
{
    public class JobProjectViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "Invalid Job ID")]
        public int JobId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}