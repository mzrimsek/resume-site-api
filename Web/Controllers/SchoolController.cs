using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Web.ActionFilters;
using Web.Mappers.SchoolMappers;
using Web.Models.SchoolModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await _schoolRepository.GetAll();
            var schoolViews = SchoolViewModelMapper.MapFrom(schools); ;
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

            var schoolViewModel = SchoolViewModelMapper.MapFrom(school);
            return Ok(schoolViewModel);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolViewModel entity)
        {
            var domainModel = SchoolDomainModelMapper.MapFrom(entity);
            var savedSchool = await _schoolRepository.Save(domainModel);
            var schoolViewModel = SchoolViewModelMapper.MapFrom(savedSchool);

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

            var viewModel = SchoolViewModelMapper.MapFrom(id, entity);
            var domainModel = SchoolDomainModelMapper.MapFrom(viewModel);

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