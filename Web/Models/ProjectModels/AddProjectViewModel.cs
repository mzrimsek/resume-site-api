using System.ComponentModel.DataAnnotations;

namespace Web.Models.ProjectModels
{
    public class AddProjectViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        [Required]
        public string Source { get; set; }
    }
}