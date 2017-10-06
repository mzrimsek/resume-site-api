using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Integration.EntityFramework.Models
{
    public class SkillDatabaseModel
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