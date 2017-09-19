using System;
using System.ComponentModel.DataAnnotations;

namespace Integration.EntityFramework.Models
{
  public class Job
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
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime EndDate;
  }
}