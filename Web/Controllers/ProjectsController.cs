using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.ProjectModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly ControllerRequestHelper<ProjectDomainModel, ProjectViewModel> _controllerRequestHelper;
        public ProjectsController(IProjectRepository projectRepository, IMapper mapper)
        {
            _controllerRequestHelper = new ControllerRequestHelper<ProjectDomainModel, ProjectViewModel>(projectRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> GetProject(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddProject([FromBody] AddProjectViewModel entity)
        {
            return await _controllerRequestHelper.Add(entity, "GetProject");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectViewModel entity)
        {
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}