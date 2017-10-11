using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Integration.EntityFramework.Models
{
    public class ProjectDatabaseModel : IHasId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        [Required]
        public string Source { get; set; }
    }
}