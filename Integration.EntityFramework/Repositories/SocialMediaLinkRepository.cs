using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class SocialMediaLinkRepository : ISocialMediaLinkRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<SocialMediaLinkDomainModel, SocialMediaLinkDatabaseModel> _repositoryHelper;
        public SocialMediaLinkRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<SocialMediaLinkDomainModel, SocialMediaLinkDatabaseModel>(databaseContext.SocialMediaLinks, mapper);
        }
        
        public async Task<IEnumerable<SocialMediaLinkDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<SocialMediaLinkDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<SocialMediaLinkDomainModel> Save(SocialMediaLinkDomainModel entity)
        {
            var socialMediaLink = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return socialMediaLink;
        }

        public async void Delete(int id)
        {
            _repositoryHelper.Delete(id);
            await _databaseContext.SaveChangesAsync();
        }
    }
}