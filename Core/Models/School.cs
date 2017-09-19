using System;

namespace Core.Models
{
  public class School
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}