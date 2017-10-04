using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.SchoolModels
{
    public class UpdateSchoolViewModel : AddSchoolViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}