using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.SocialMediaLinkModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SocialMediaLinksController : Controller
    {
        private readonly ControllerRequestHelper<SocialMediaLinkDomainModel, SocialMediaLinkViewModel> _controllerRequestHelper;
        public SocialMediaLinksController(ISocialMediaLinkRepository socialMediaLinkRepository, IMapper mapper)
        {
            _controllerRequestHelper = new ControllerRequestHelper<SocialMediaLinkDomainModel, SocialMediaLinkViewModel>(socialMediaLinkRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSocialMediaLinks()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetSocialMediaLink")]
        public async Task<IActionResult> GetSocialMediaLink(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddSocialMediaLink([FromBody] AddSocialMediaLinkViewModel entity)
        {
            return await _controllerRequestHelper.Add(entity, "GetSocialMediaLink");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateSocialMediaLink(int id, [FromBody] UpdateSocialMediaLinkViewModel entity)
        {
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMediaLink(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}