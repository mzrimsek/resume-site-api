using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<SchoolDomainModel, SchoolDatabaseModel> _repositoryHelper;
        public SchoolRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<SchoolDomainModel, SchoolDatabaseModel>(databaseContext.Schools, mapper);
        }

        public async Task<IEnumerable<SchoolDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<SchoolDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<SchoolDomainModel> Save(SchoolDomainModel entity)
        {
            var school = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return school;
        }

        public async Task Delete(int id)
        {
            await _repositoryHelper.Delete(id);
            await _databaseContext.SaveChangesAsync();
        }
    }
}