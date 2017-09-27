using System.ComponentModel.DataAnnotations;

namespace Web.Models.JobProjectModels
{
    public class AddUpdateJobProjectViewModel
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}