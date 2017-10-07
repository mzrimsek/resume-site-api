using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.SkillModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;
        private readonly ControllerRequestHelper<SkillDomainModel, SkillViewModel> _controllerRequestHelper;
        public SkillsController(ISkillRepository skillRepository, ILanguageRepository languageRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _controllerRequestHelper = new ControllerRequestHelper<SkillDomainModel, SkillViewModel>(skillRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetSkill")]
        public async Task<IActionResult> GetSkill(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpGet("language/{languageId}")]
        public async Task<IActionResult> GetSkillsForLanguage(int languageId)
        {
            var language = await _languageRepository.GetById(languageId);
            if (language == null)
            {
                return NotFound();
            }

            var skills = await _skillRepository.GetByLanguageId(languageId);
            var skillViews = _mapper.Map<IEnumerable<SkillViewModel>>(skills);
            return Ok(skillViews);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddSkill([FromBody] AddSkillViewModel entity)
        {
            var language = await _languageRepository.GetById(entity.LanguageId);
            if (language == null)
            {
                return BadRequest("Language does not exist.");
            }
            return await _controllerRequestHelper.Add(entity, "GetSkill");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] UpdateSkillViewModel entity)
        {
            var language = await _languageRepository.GetById(entity.LanguageId);
            if (language == null)
            {
                return BadRequest("Language does not exist.");
            }
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}