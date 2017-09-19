using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
  public interface IJobProjectRepository
  {
    IEnumerable<JobProject> GetAll();
    JobProject GetById(int id);
    JobProject GetByJobId(int jobId);
  }
}