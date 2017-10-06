using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Interfaces.RepositoryInterfaces;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.SchoolModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SchoolsController : Controller
    {
        private readonly ControllerRequestHelper<SchoolDomainModel, SchoolViewModel> _controllerRequestHelper;
        public SchoolsController(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _controllerRequestHelper = new ControllerRequestHelper<SchoolDomainModel, SchoolViewModel>(schoolRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetSchool")]
        public async Task<IActionResult> GetSchool(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolViewModel entity)
        {
            return await _controllerRequestHelper.Add(entity, "GetSchool");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] UpdateSchoolViewModel entity)
        {
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}