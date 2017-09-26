using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;
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

        public IEnumerable<JobProjectDomainModel> GetAll()
        {
            var jobProjects = _databaseContext.JobProjects.ToList();
            return JobProjectDomainModelMapper.MapFrom(jobProjects);
        }

        public JobProjectDomainModel GetById(int id)
        {
            var jobProject = _databaseContext.JobProjects.SingleOrDefault(x => x.Id == id);
            return jobProject == null ? null : JobProjectDomainModelMapper.MapFrom(jobProject);
        }

        public IEnumerable<JobProjectDomainModel> GetByJobId(int jobId)
        {
            var jobProjects = _databaseContext.JobProjects.Where(x => x.JobId == jobId);
            return JobProjectDomainModelMapper.MapFrom(jobProjects);
        }

        public JobProjectDomainModel Save(JobProjectDomainModel jobProject)
        {
            var databaseModel = JobProjectDatabaseModelMapper.MapFrom(jobProject);

            _databaseContext.Add(databaseModel);
            _databaseContext.SaveChanges();

            return JobProjectDomainModelMapper.MapFrom(databaseModel);
        }

        public void Delete(int id)
        {
            var jobProjectToDelete = _databaseContext.JobProjects.SingleOrDefault(x => x.Id == id);
            if (jobProjectToDelete != null)
            {
                _databaseContext.Remove(jobProjectToDelete);
                _databaseContext.SaveChanges();
            }
        }

        public JobProjectDomainModel Update(JobProjectDomainModel jobProject)
        {
            var databaseModel = JobProjectDatabaseModelMapper.MapFrom(jobProject);
            var existingModel = _databaseContext.JobProjects.SingleOrDefault(x => x.Id == jobProject.Id);
            existingModel = databaseModel;

            _databaseContext.SaveChanges();

            return JobProjectDomainModelMapper.MapFrom(existingModel);
        }
    }
}