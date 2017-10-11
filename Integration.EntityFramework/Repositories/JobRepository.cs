using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<JobDomainModel, JobDatabaseModel> _repositoryHelper;
        public JobRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<JobDomainModel, JobDatabaseModel>(databaseContext.Jobs, mapper);
        }

        public async Task<IEnumerable<JobDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<JobDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<JobDomainModel> Save(JobDomainModel entity)
        {
            var job = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return job;
        }

        public async Task Delete(int id)
        {
            var jobToDelete = await _databaseContext.Jobs.SingleOrDefaultAsync(x => x.Id == id);
            if (jobToDelete != null)
            {
                var jobProjectsForJob = await _databaseContext.JobProjects.Where(x => x.JobId == id).ToListAsync();
                _databaseContext.RemoveRange(jobProjectsForJob);
                _databaseContext.Remove(jobToDelete);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}