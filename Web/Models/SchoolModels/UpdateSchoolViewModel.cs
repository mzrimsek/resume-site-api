using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.SchoolModels
{
    public class UpdateSchoolViewModel : AddSchoolViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}