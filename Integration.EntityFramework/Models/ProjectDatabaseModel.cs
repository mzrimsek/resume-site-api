using System.ComponentModel.DataAnnotations;

namespace Integration.EntityFramework.Models
{
    public class ProjectDatabaseModel
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