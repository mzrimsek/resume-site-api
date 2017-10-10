using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Integration.EntityFramework.Models
{
    public class JobProjectDatabaseModel : IHasId
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