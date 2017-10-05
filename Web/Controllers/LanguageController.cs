using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Web.ActionFilters;
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
        [ModelStateValidation]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageViewModel entity)
        {
            var domainModel = LanguageDomainModelMapper.MapFrom(entity);
            var savedLanguage = await _languageRepository.Save(domainModel);
            var languageViewModel = LanguageViewModelMapper.MapFrom(savedLanguage);

            return CreatedAtRoute("GetLanguage", new { id = languageViewModel.Id }, languageViewModel);
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] UpdateLanguageViewModel entity)
        {
            var foundLanguage = await _languageRepository.GetById(id);
            if (foundLanguage == null)
            {
                return NotFound();
            }

            var viewModel = LanguageViewModelMapper.MapFrom(id, entity);
            var domainModel = LanguageDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = await _languageRepository.Save(domainModel);

            var updatedViewModel = LanguageViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var language = await _languageRepository.GetById(id);
            if (language != null)
            {
                _languageRepository.Delete(id);
            }
            return NoContent();
        }
    }
}