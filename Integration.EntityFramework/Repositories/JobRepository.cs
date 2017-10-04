using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<JobDomainModel>> GetAll()
        {
            var jobs = await _databaseContext.Jobs.ToListAsync();
            return JobDomainModelMapper.MapFrom(jobs);
        }

        public async Task<JobDomainModel> GetById(int id)
        {
            var job = await _databaseContext.Jobs.SingleOrDefaultAsync(x => x.Id == id);
            return job == null ? null : JobDomainModelMapper.MapFrom(job);
        }

        public async Task<JobDomainModel> Save(JobDomainModel job)
        {
            var databaseModel = JobDatabaseModelMapper.MapFrom(job);
            var existingModel = await _databaseContext.Jobs.SingleOrDefaultAsync(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                await _databaseContext.AddAsync(databaseModel);
            }
            else
            {
                existingModel.Name = databaseModel.Name;
                existingModel.City = databaseModel.City;
                existingModel.State = databaseModel.State;
                existingModel.Title = databaseModel.Title;
                existingModel.StartDate = databaseModel.StartDate;
                existingModel.EndDate = databaseModel.EndDate;

                _databaseContext.Update(existingModel);
            }

            await _databaseContext.SaveChangesAsync();
            return JobDomainModelMapper.MapFrom(databaseModel);
        }

        public void Delete(int id)
        {
            var jobToDelete = _databaseContext.Jobs.SingleOrDefault(x => x.Id == id);
            if (jobToDelete != null)
            {
                var jobProjectsForJob = _databaseContext.JobProjects.Where(x => x.JobId == jobToDelete.Id);
                _databaseContext.RemoveRange(jobProjectsForJob);
                _databaseContext.Remove(jobToDelete);
                _databaseContext.SaveChanges();
            }
        }
    }
}