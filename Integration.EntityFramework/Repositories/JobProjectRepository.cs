using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class JobProjectRepository : IJobProjectRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public JobProjectRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobProjectDomainModel>> GetAll()
        {
            return await _databaseContext.JobProjects.Where(x => true).ProjectTo<JobProjectDomainModel>().ToListAsync();
        }

        public async Task<JobProjectDomainModel> GetById(int id)
        {
            var jobProject = await _databaseContext.JobProjects.Where(x => x.Id == id).ProjectTo<JobProjectDomainModel>().FirstOrDefaultAsync();
            return jobProject == null ? null : jobProject;
        }

        public async Task<IEnumerable<JobProjectDomainModel>> GetByJobId(int jobId)
        {
            return await _databaseContext.JobProjects.Where(x => x.JobId == jobId).ProjectTo<JobProjectDomainModel>().ToListAsync();
        }

        public async Task<JobProjectDomainModel> Save(JobProjectDomainModel entity)
        {
            var databaseModel = _mapper.Map<JobProjectDatabaseModel>(entity);
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
            return _mapper.Map<JobProjectDomainModel>(databaseModel);
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