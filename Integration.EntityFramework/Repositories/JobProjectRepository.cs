using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Integration.EntityFramework.Mappers.JobProjectMappers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class JobProjectRepository : IJobProjectRepository
    {
        private readonly DatabaseContext _databaseContext;

        public JobProjectRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<Core.Models.JobProject> GetAll()
        {
            var jobProjects = _databaseContext.JobProjects.ToList();
            return JobProjectDomainModelMapper.MapFrom(jobProjects);
        }

        public Core.Models.JobProject GetById(int id)
        {
            var jobProject = _databaseContext.JobProjects.SingleOrDefault(x => x.Id == id);
            return jobProject == null ? null : JobProjectDomainModelMapper.MapFrom(jobProject);
        }

        public IEnumerable<Core.Models.JobProject> GetByJobId(int jobId)
        {
            var jobProjects = _databaseContext.JobProjects.Where(x => x.JobId == jobId);
            return JobProjectDomainModelMapper.MapFrom(jobProjects);
        }
    }
}