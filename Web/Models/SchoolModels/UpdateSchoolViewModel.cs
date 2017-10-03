using System.ComponentModel.DataAnnotations;

namespace Web.Models.SchoolModels
{
    public class UpdateSchoolViewModel : AddSchoolViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}