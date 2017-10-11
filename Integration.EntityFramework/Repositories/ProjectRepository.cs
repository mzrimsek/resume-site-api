using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<ProjectDomainModel, ProjectDatabaseModel> _repositoryHelper;
        public ProjectRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<ProjectDomainModel, ProjectDatabaseModel>(databaseContext.Projects, mapper);
        }
        
        public async Task<IEnumerable<ProjectDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<ProjectDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<ProjectDomainModel> Save(ProjectDomainModel entity)
        {
            var project = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return project;
        }

        public async Task Delete(int id)
        {
            await _repositoryHelper.Delete(id);
            await _databaseContext.SaveChangesAsync();
        }
    }
}