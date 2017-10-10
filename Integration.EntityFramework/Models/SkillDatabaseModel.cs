using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Integration.EntityFramework.Models
{
    public class SkillDatabaseModel : IHasId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
        [ForeignKey("LanguageId")]
        public LanguageDatabaseModel Language { get; set; }
    }
}