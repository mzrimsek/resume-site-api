using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Integration.EntityFramework.Models
{
    public class LanguageDatabaseModel : IHasId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}