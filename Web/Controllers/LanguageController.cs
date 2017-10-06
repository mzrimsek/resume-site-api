using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Interfaces.RepositoryInterfaces;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.LanguageModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly ControllerRequestHelper<LanguageDomainModel, LanguageViewModel> _controllerRequestHelper;
        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            _controllerRequestHelper = new ControllerRequestHelper<LanguageDomainModel, LanguageViewModel>(languageRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetLanguage")]
        public async Task<IActionResult> GetLanguage(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageViewModel entity)
        {
            return await _controllerRequestHelper.Add(entity, "GetLanguage");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] UpdateLanguageViewModel entity)
        {
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}