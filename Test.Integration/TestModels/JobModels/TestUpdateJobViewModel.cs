using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.JobModels
{
    public class TestUpdateJobViewModel : TestAddJobViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}