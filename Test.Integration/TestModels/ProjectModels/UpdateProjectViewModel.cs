using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Test.Integration.TestModels.ProjectModels
{
    public class UpdateProjectViewModel : AddProjectViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}