using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Web.Models.LanguageModels
{
    public class UpdateLanguageViewModel : AddLanguageViewModel, IHasId
    {
        [Required]
        public int Id { get; set; }
    }
}