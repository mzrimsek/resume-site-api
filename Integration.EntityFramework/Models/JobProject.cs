using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Integration.EntityFramework.Models
{
    public class JobProjectDatabaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("JobId")]
        public JobDatabaseModel Job { get; set; }
    }
}