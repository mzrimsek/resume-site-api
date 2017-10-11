using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.ProjectModels
{
    public class UpdateProjectViewModel : AddProjectViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}