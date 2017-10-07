using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.SkillModels
{
    public class AddSkillViewModel
    {
        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "Invalid Language ID")]
        public int LanguageId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Invalid Rating - Must Be 1, 2, or 3")]
        public int Rating { get; set; }
    }
}