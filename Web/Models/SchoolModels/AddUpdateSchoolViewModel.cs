using System.ComponentModel.DataAnnotations;

namespace Web.Models.SchoolModels
{
    public class AddUpdateSchoolViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}