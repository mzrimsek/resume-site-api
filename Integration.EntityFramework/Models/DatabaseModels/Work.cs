using System;
using System.ComponentModel.DataAnnotations;

namespace Integration.EntityFramework.Models.DatabaseModels
{
    public class Work
    {
        [Key]
        public int Id;
        [Required]
        public string Name;
        [Required]
        public string City;
        [Required]
        public string State;
        [Required]
        public string Title;
        [Required]
        public DateTime StartDate;
        public DateTime EndDate;
    }
}