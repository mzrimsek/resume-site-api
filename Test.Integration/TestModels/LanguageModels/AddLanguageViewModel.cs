using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.LanguageModels
{
    public class AddLanguageViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Invalid Rating - Must Be 1, 2, or 3")]
        public int Rating { get; set; }
    }
}