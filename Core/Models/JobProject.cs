namespace Core.Models
{
  public class JobProject
  {
    public int Id { get; set; }
    public int JobId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}