using System;
using System.ComponentModel.DataAnnotations;

namespace Integration.EntityFramework.Models
{
    public class SchoolDatabaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Major { get; set; }
        public string Degree { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}