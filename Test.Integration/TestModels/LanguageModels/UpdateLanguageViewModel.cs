using System.ComponentModel.DataAnnotations;

namespace Test.Integration.TestModels.LanguageModels
{
    public class UpdateLanguageViewModel : AddLanguageViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}