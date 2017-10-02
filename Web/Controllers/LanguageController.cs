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
            return new EmptyResult();
        }

        [HttpGet("{id}", Name = "GetLanguage")]
        public IActionResult GetLanguage(int id)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult AddLanguage([FromBody] AddUpdateLanguageViewModel language)
        {
            return new EmptyResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            return new EmptyResult();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, [FromBody] AddUpdateLanguageViewModel languageViewModel)
        {
            return new EmptyResult();
        }
    }
}