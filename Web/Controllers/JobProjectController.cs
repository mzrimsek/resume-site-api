using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Web.ActionFilters;
using Web.Models.JobProjectModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobProjectController : Controller
    {
        private readonly IJobProjectRepository _jobProjectRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public JobProjectController(IJobProjectRepository jobProjectRepository, IJobRepository jobRepository, IMapper mapper)
        {
            _jobProjectRepository = jobProjectRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobProjects()
        {
            var jobProjects = await _jobProjectRepository.GetAll();
            var jobProjectViews = _mapper.Map<IEnumerable<JobProjectViewModel>>(jobProjects);
            return Ok(jobProjectViews);
        }

        [HttpGet("{id}", Name = "GetJobProject")]
        public async Task<IActionResult> GetJobProject(int id)
        {
            var jobProject = await _jobProjectRepository.GetById(id);
            if (jobProject == null)
            {
                return NotFound();
            }

            var jobProjectViewModel = _mapper.Map<JobProjectViewModel>(jobProject);
            return Ok(jobProjectViewModel);
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
                return BadRequest("JobId is not valid.");
            }

            var domainModel = _mapper.Map<JobProjectDomainModel>(entity);
            var savedJobProject = await _jobProjectRepository.Save(domainModel);
            var jobProjectViewModel = _mapper.Map<JobProjectViewModel>(savedJobProject);

            return CreatedAtRoute("GetJobProject", new { id = jobProjectViewModel.Id }, jobProjectViewModel);
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateJobProject(int id, [FromBody] UpdateJobProjectViewModel entity)
        {
            var job = await _jobRepository.GetById(entity.JobId);
            if (job == null)
            {
                return BadRequest("JobId is not valid.");
            }
            var foundJobProject = await _jobProjectRepository.GetById(id);
            if (foundJobProject == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<JobProjectViewModel>(entity);
            var domainModel = _mapper.Map<JobProjectDomainModel>(viewModel);

            await _jobProjectRepository.Save(domainModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobProject(int id)
        {
            var jobProject = await _jobProjectRepository.GetById(id);
            if (jobProject != null)
            {
                _jobProjectRepository.Delete(id);
            }
            return NoContent();
        }
    }
}