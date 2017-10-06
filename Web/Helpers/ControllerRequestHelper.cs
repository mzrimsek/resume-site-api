using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces;

namespace Web.Helpers
{
    public class ControllerRequestHelper<DomainModelType, ViewModelType>
    {
        private readonly IRepository<DomainModelType> _repository;
        private readonly IMapper _mapper;
        public ControllerRequestHelper(IRepository<DomainModelType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAll();
            var views = _mapper.Map<IEnumerable<ViewModelType>>(entities);
            return new OkObjectResult(views);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                return new NotFoundObjectResult(null);
            }

            var view = _mapper.Map<ViewModelType>(entity);
            return new OkObjectResult(view);
        }

        public async Task<IActionResult> Add(object entity, string createdRouteName)
        {
            var domain = _mapper.Map<DomainModelType>(entity);
            var saved = await _repository.Save(domain);
            var view = _mapper.Map<ViewModelType>(saved) as IHasId;

            return new CreatedAtRouteResult(createdRouteName, new { id = view.Id }, view);
        }

        public async Task<IActionResult> Update(int id, object entity)
        {
            var found = await _repository.GetById(id);
            if (found == null)
            {
                return new NotFoundObjectResult(null);
            }

            var view = _mapper.Map<ViewModelType>(entity);
            var domain = _mapper.Map<DomainModelType>(view);

            await _repository.Save(domain);
            return new NoContentResult();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity != null)
            {
                _repository.Delete(id);
            }
            return new NoContentResult();
        }
    }
}