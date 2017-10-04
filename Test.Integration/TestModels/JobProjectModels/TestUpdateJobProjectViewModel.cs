using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.JobProjectModels
{
    public class TestUpdateJobProjectViewModel : TestAddJobProjectViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}