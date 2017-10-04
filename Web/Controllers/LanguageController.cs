using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages = await _languageRepository.GetAll();
            var languageViews = LanguageViewModelMapper.MapFrom(languages);
            return Ok(languageViews);
        }

        [HttpGet("{id}", Name = "GetLanguage")]
        public async Task<IActionResult> GetLanguage(int id)
        {
            var language = await _languageRepository.GetById(id);
            if (language == null)
            {
                return NotFound();
            }
            var languageViewModel = LanguageViewModelMapper.MapFrom(language);
            return Ok(languageViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageViewModel language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var domainModel = LanguageDomainModelMapper.MapFrom(language);
            var savedLanguage = await _languageRepository.Save(domainModel);
            var languageViewModel = LanguageViewModelMapper.MapFrom(savedLanguage);

            return CreatedAtRoute("GetLanguage", new { id = languageViewModel.Id }, languageViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] UpdateLanguageViewModel language)
        {
            if (!ModelState.IsValid || id != language.Id)
            {
                return BadRequest(ModelState);
            }
            var foundLanguage = await _languageRepository.GetById(id);
            if (foundLanguage == null)
            {
                return NotFound();
            }

            var viewModel = LanguageViewModelMapper.MapFrom(id, language);
            var domainModel = LanguageDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = await _languageRepository.Save(domainModel);

            var updatedViewModel = LanguageViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var language = await _languageRepository.GetById(id);
            if (language == null)
            {
                return NotFound();
            }

            _languageRepository.Delete(id);
            return NoContent();
        }
    }
}