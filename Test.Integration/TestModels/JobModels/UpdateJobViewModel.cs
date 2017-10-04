using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.JobModels
{
    public class UpdateJobViewModel : AddJobViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}