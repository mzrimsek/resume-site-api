using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Helpers
{
    public class RepositoryHelper<TDomainModel, TDatabaseModel> where TDatabaseModel : class, IHasId
    {
        private readonly DbSet<TDatabaseModel> _dbSet;
        private readonly IMapper _mapper;
        public RepositoryHelper(DbSet<TDatabaseModel> dbSet, IMapper mapper)
        {
            _dbSet = dbSet;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDomainModel>> GetAll()
        {
            return await _dbSet.Where(x => true).ProjectTo<TDomainModel>().ToListAsync();
        }

        public async Task<TDomainModel> GetById(int id)
        {
            return await _dbSet.Where(x => x.Id == id).ProjectTo<TDomainModel>().FirstOrDefaultAsync(); 
        }
        
        public async Task<TDomainModel> Save(TDomainModel entity)
        {
            var databaseModel = _mapper.Map<TDatabaseModel>(entity);
            var existingModel = await _dbSet.SingleOrDefaultAsync(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                await _dbSet.AddAsync(databaseModel);
            }
            else
            {
                _mapper.Map(databaseModel, existingModel);
                _dbSet.Update(existingModel);
            }
            return _mapper.Map<TDomainModel>(databaseModel);
        }

        public async void Delete(int id)
        {
            var entityToDelete = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
        }
    }
}