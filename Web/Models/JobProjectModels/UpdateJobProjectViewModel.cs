using System.ComponentModel.DataAnnotations;

namespace Web.Models.JobProjectModels
{
    public class UpdateJobProjectViewModel : AddJobProjectViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}