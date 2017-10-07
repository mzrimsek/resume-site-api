using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Web.ActionFilters;
using Web.Helpers;
using Web.Models.JobProjectModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobProjectsController : Controller
    {
        private readonly IJobProjectRepository _jobProjectRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly ControllerRequestHelper<JobProjectDomainModel, JobProjectViewModel> _controllerRequestHelper;
        public JobProjectsController(IJobProjectRepository jobProjectRepository, IJobRepository jobRepository, IMapper mapper)
        {
            _jobProjectRepository = jobProjectRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
            _controllerRequestHelper = new ControllerRequestHelper<JobProjectDomainModel, JobProjectViewModel>(jobProjectRepository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobProjects()
        {
            return await _controllerRequestHelper.GetAll();
        }

        [HttpGet("{id}", Name = "GetJobProject")]
        public async Task<IActionResult> GetJobProject(int id)
        {
            return await _controllerRequestHelper.GetById(id);
        }

        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetJobProjectsForJob(int jobId)
        {
            var job = await _jobRepository.GetById(jobId);
            if (job == null)
            {
                return NotFound();
            }

            var jobProjects = await _jobProjectRepository.GetByJobId(jobId);
            var jobProjectViews = _mapper.Map<IEnumerable<JobProjectViewModel>>(jobProjects);
            return Ok(jobProjectViews);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddJobProject([FromBody] AddJobProjectViewModel entity)
        {
            var job = await _jobRepository.GetById(entity.JobId);
            if (job == null)
            {
                return BadRequest("Job does not exist");
            }
            return await _controllerRequestHelper.Add(entity, "GetJobProject");
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateJobProject(int id, [FromBody] UpdateJobProjectViewModel entity)
        {
            var job = await _jobRepository.GetById(entity.JobId);
            if (job == null)
            {
                return BadRequest("Job does not exist.");
            }
            return await _controllerRequestHelper.Update(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobProject(int id)
        {
            return await _controllerRequestHelper.Delete(id);
        }
    }
}