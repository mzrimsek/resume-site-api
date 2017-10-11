using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Repositories
{
    public class JobProjectRepository : IJobProjectRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<JobProjectDomainModel, JobProjectDatabaseModel> _repositoryHelper;
        public JobProjectRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<JobProjectDomainModel, JobProjectDatabaseModel>(databaseContext.JobProjects, mapper);
        }

        public async Task<IEnumerable<JobProjectDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<JobProjectDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<IEnumerable<JobProjectDomainModel>> GetByJobId(int jobId)
        {
            return await _databaseContext.JobProjects.Where(x => x.JobId == jobId).ProjectTo<JobProjectDomainModel>().ToListAsync();
        }

        public async Task<JobProjectDomainModel> Save(JobProjectDomainModel entity)
        {
            var jobProject = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return jobProject;
        }

        public async Task Delete(int id)
        {
            await _repositoryHelper.Delete(id);
            await _databaseContext.SaveChangesAsync();
        }
    }
}