using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.JobProjectModels
{
    public class UpdateJobProjectViewModel : AddJobProjectViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}