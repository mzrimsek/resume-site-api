using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.JobModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private readonly ControllerRequestHelper<JobDomainModel, JobViewModel> _controllerRequestHelper;
        public JobsController(IJobRepository jobRepository, IMapper mapper)
        {
            _controllerRequestHelper = new ControllerRequestHelper<JobDomainModel, JobViewModel>(jobRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetJob")]
        public async Task<IActionResult> GetJob(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddJob([FromBody] AddJobViewModel entity)
        {
            return await _controllerRequestHelper.Add(entity, "GetJob");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] UpdateJobViewModel entity)
        {
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}