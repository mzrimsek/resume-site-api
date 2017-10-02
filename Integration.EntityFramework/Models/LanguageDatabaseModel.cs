using System.ComponentModel.DataAnnotations;

namespace Integration.EntityFramework.Models
{
    public class LanguageDatabaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}