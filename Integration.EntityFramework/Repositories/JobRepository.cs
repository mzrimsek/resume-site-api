using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Integration.EntityFramework.Mappers.JobMappers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DatabaseContext _databaseContext;

        public JobRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<Core.Models.Job> GetAll()
        {
            var jobs = _databaseContext.Jobs.ToList();
            return JobDomainModelMapper.MapFrom(jobs);
        }

        public Core.Models.Job GetById(int id)
        {
            var job = _databaseContext.Jobs.SingleOrDefault(x => x.Id == id);
            return job == null ? null : JobDomainModelMapper.MapFrom(job);
        }
    }
}