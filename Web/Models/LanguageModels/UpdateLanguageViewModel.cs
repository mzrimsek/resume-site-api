using System.ComponentModel.DataAnnotations;

namespace Web.Models.LanguageModels
{
    public class UpdateLanguageViewModel : AddLanguageViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}