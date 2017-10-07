using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public JobRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDomainModel>> GetAll()
        {
            return await _databaseContext.Jobs.Where(x => true).ProjectTo<JobDomainModel>().ToListAsync();
        }

        public async Task<JobDomainModel> GetById(int id)
        {
            return await _databaseContext.Jobs.Where(x => x.Id == id).ProjectTo<JobDomainModel>().FirstOrDefaultAsync();
        }

        public async Task<JobDomainModel> Save(JobDomainModel entity)
        {
            var databaseModel = _mapper.Map<JobDatabaseModel>(entity);
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
            return _mapper.Map<JobDomainModel>(databaseModel);
        }

        public void Delete(int id)
        {
            var jobToDelete = _databaseContext.Jobs.SingleOrDefault(x => x.Id == id);
            if (jobToDelete != null)
            {
                var jobProjectsForJob = _databaseContext.JobProjects.Where(x => x.JobId == id);
                _databaseContext.RemoveRange(jobProjectsForJob);
                _databaseContext.Remove(jobToDelete);
                _databaseContext.SaveChanges();
            }
        }
    }
}