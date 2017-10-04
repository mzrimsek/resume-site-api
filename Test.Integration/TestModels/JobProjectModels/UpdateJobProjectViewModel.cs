using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.JobProjectModels
{
    public class UpdateJobProjectViewModel : AddJobProjectViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}