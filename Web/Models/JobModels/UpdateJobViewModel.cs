using System.ComponentModel.DataAnnotations;

namespace Web.Models.JobModels
{
    public class UpdateJobViewModel : AddJobViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}