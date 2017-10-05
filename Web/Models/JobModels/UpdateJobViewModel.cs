using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.JobModels
{
    public class UpdateJobViewModel : AddJobViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}