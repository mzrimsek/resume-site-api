using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IJobRepository
    {
        IEnumerable<JobDomainModel> GetAll();
        JobDomainModel GetById(int id);
        JobDomainModel Save(JobDomainModel job);
        JobDomainModel Delete(int id);
    }
}