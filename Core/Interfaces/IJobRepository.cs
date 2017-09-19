using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
  public interface IJobRepository
  {
    IEnumerable<Job> GetAll();
    Job GetById(int id);
  }
}