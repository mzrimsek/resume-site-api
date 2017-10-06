using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Interfaces.RepositoryInterfaces;
using Web.ActionFilters;
using Web.Models.SchoolModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        public SchoolController(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await _schoolRepository.GetAll();
            var schoolViews = _mapper.Map<IEnumerable<SchoolViewModel>>(schools); ;
            return Ok(schoolViews);
        }

        [HttpGet("{id}", Name = "GetSchool")]
        public async Task<IActionResult> GetSchool(int id)
        {
            var school = await _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }

            var schoolViewModel = _mapper.Map<SchoolViewModel>(school);
            return Ok(schoolViewModel);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolViewModel entity)
        {
            var domainModel = _mapper.Map<SchoolDomainModel>(entity);
            var savedSchool = await _schoolRepository.Save(domainModel);
            var schoolViewModel = _mapper.Map<SchoolViewModel>(savedSchool);

            return CreatedAtRoute("GetSchool", new { id = schoolViewModel.Id }, schoolViewModel);
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] UpdateSchoolViewModel entity)
        {
            var foundSchool = await _schoolRepository.GetById(id);
            if (foundSchool == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<SchoolViewModel>(entity);
            var domainModel = _mapper.Map<SchoolDomainModel>(viewModel);

            await _schoolRepository.Save(domainModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _schoolRepository.GetById(id);
            if (school != null)
            {
                _schoolRepository.Delete(id);
            }
            return NoContent();
        }
    }
}