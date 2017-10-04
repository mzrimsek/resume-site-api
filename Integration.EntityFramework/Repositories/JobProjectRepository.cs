using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<JobProjectDomainModel>> GetAll()
        {
            var jobProjects = await _databaseContext.JobProjects.ToListAsync();
            return JobProjectDomainModelMapper.MapFrom(jobProjects);
        }

        public async Task<JobProjectDomainModel> GetById(int id)
        {
            var jobProject = await _databaseContext.JobProjects.SingleOrDefaultAsync(x => x.Id == id);
            return jobProject == null ? null : JobProjectDomainModelMapper.MapFrom(jobProject);
        }

        public async Task<IEnumerable<JobProjectDomainModel>> GetByJobId(int jobId)
        {
            var jobProjects = await _databaseContext.JobProjects.Where(x => x.JobId == jobId).ToListAsync();
            return JobProjectDomainModelMapper.MapFrom(jobProjects);
        }

        public async Task<JobProjectDomainModel> Save(JobProjectDomainModel jobProject)
        {
            var databaseModel = JobProjectDatabaseModelMapper.MapFrom(jobProject);
            var existingModel = await _databaseContext.JobProjects.SingleOrDefaultAsync(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                await _databaseContext.AddAsync(databaseModel);
            }
            else
            {
                existingModel.JobId = databaseModel.JobId;
                existingModel.Name = databaseModel.Name;
                existingModel.Description = databaseModel.Description;

                _databaseContext.Update(existingModel);
            }

            await _databaseContext.SaveChangesAsync();
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
    }
}