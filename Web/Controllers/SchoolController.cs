using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
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
        public IActionResult GetAllSchools()
        {
            var schools = _schoolRepository.GetAll();
            var schoolViews = SchoolViewModelMapper.MapFrom(schools); ;
            return Ok(schoolViews);
        }

        [HttpGet("{id}", Name = "GetSchool")]
        public IActionResult GetSchool(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }

            var schoolViewModel = SchoolViewModelMapper.MapFrom(school);
            return Ok(schoolViewModel);
        }

        [HttpPost]
        public IActionResult AddSchool([FromBody] AddUpdateSchoolViewModel school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var domainModel = SchoolDomainModelMapper.MapFrom(school);
            var savedSchool = _schoolRepository.Save(domainModel);
            var schoolViewModel = SchoolViewModelMapper.MapFrom(savedSchool);

            return CreatedAtRoute("GetSchool", new { id = schoolViewModel.Id }, schoolViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchool(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }

            _schoolRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSchool(int id, [FromBody] AddUpdateSchoolViewModel school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var foundSchool = _schoolRepository.GetById(id);
            if (foundSchool == null)
            {
                return NotFound();
            }

            var viewModel = SchoolViewModelMapper.MapFrom(id, school);
            var domainModel = SchoolDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = _schoolRepository.Update(domainModel);

            var updatedViewModel = SchoolViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }
    }
}