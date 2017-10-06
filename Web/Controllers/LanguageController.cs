using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Interfaces.RepositoryInterfaces;
using Web.ActionFilters;
using Web.Models.LanguageModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;
        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages = await _languageRepository.GetAll();
            var languageViews = _mapper.Map<IEnumerable<LanguageViewModel>>(languages);
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
            var languageViewModel = _mapper.Map<LanguageViewModel>(language);
            return Ok(languageViewModel);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageViewModel entity)
        {
            var domainModel = _mapper.Map<LanguageDomainModel>(entity);
            var savedLanguage = await _languageRepository.Save(domainModel);
            var languageViewModel = _mapper.Map<LanguageViewModel>(savedLanguage);

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

            var viewModel = _mapper.Map<LanguageViewModel>(entity);
            var domainModel = _mapper.Map<LanguageDomainModel>(viewModel);

            await _languageRepository.Save(domainModel);
            return NoContent();
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