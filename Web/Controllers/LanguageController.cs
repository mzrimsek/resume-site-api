using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Web.Mappers.LanguageMappers;
using Web.Models.LanguageModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public IActionResult GetAllLanguages()
        {
            var languages = _languageRepository.GetAll();
            var languageViews = LanguageViewModelMapper.MapFrom(languages);
            return Ok(languageViews);
        }

        [HttpGet("{id}", Name = "GetLanguage")]
        public IActionResult GetLanguage(int id)
        {
            var language = _languageRepository.GetById(id);
            if (language == null)
            {
                return NotFound();
            }
            var languageViewModel = LanguageViewModelMapper.MapFrom(language);
            return Ok(languageViewModel);
        }

        [HttpPost]
        public IActionResult AddLanguage([FromBody] AddUpdateLanguageViewModel language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var domainModel = LanguageDomainModelMapper.MapFrom(language);
            var savedLanguage = _languageRepository.Save(domainModel);
            var languageViewModel = LanguageViewModelMapper.MapFrom(savedLanguage);

            return CreatedAtRoute("GetLanguage", new { id = languageViewModel.Id }, languageViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, [FromBody] AddUpdateLanguageViewModel language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var foundLanguage = _languageRepository.GetById(id);
            if (foundLanguage == null)
            {
                return NotFound();
            }

            var viewModel = LanguageViewModelMapper.MapFrom(id, language);
            var domainModel = LanguageDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = _languageRepository.Save(domainModel);

            var updatedViewModel = LanguageViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            var language = _languageRepository.GetById(id);
            if (language == null)
            {
                return NotFound();
            }

            _languageRepository.Delete(id);
            return NoContent();
        }
    }
}