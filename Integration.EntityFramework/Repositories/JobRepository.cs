using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;
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

        public IEnumerable<JobDomainModel> GetAll()
        {
            var jobs = _databaseContext.Jobs.ToList();
            return JobDomainModelMapper.MapFrom(jobs);
        }

        public JobDomainModel GetById(int id)
        {
            var job = _databaseContext.Jobs.SingleOrDefault(x => x.Id == id);
            return job == null ? null : JobDomainModelMapper.MapFrom(job);
        }

        public JobDomainModel Save(JobDomainModel job)
        {
            var databaseModel = JobDatabaseModelMapper.MapFrom(job);
            _databaseContext.Jobs.Add(databaseModel);
            _databaseContext.SaveChanges();

            return JobDomainModelMapper.MapFrom(databaseModel);
        }

        public void Delete(int id)
        {
            var jobToDelete = _databaseContext.Jobs.SingleOrDefault(x => x.Id == id);
            if (jobToDelete != null)
            {
                _databaseContext.Remove(jobToDelete);
                _databaseContext.SaveChanges();
            }
        }
    }
}