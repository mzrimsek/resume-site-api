using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces;
using Web.Mappers.JobProjectMappers;
using Web.Models.JobProjectModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobProjectController : Controller
    {
        private readonly IJobProjectRepository _jobProjectRepository;
        private readonly IJobRepository _jobRepository;
        public JobProjectController(IJobProjectRepository jobProjectRepository, IJobRepository jobRepository)
        {
            _jobProjectRepository = jobProjectRepository;
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobProjects()
        {
            var jobProjects = await _jobProjectRepository.GetAll();
            var jobProjectViews = JobProjectViewModelMapper.MapFrom(jobProjects);
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

            var jobProjectViewModel = JobProjectViewModelMapper.MapFrom(jobProject);
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
            var jobProjectViews = JobProjectViewModelMapper.MapFrom(jobProjects);
            return Ok(jobProjectViews);
        }

        [HttpPost]
        public async Task<IActionResult> AddJobProject([FromBody] AddJobProjectViewModel jobProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var job = await _jobRepository.GetById(jobProject.JobId);
            if (job == null)
            {
                return NotFound();
            }

            var domainModel = JobProjectDomainModelMapper.MapFrom(jobProject);
            var savedJobProject = await _jobProjectRepository.Save(domainModel);
            var jobProjectViewModel = JobProjectViewModelMapper.MapFrom(savedJobProject);

            return CreatedAtRoute("GetJobProject", new { id = jobProjectViewModel.Id }, jobProjectViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobProject(int id, [FromBody] UpdateJobProjectViewModel jobProject)
        {
            if (!ModelState.IsValid || id != jobProject.Id)
            {
                return BadRequest(ModelState);
            }
            var job = await _jobRepository.GetById(jobProject.JobId);
            var foundJobProject = await _jobProjectRepository.GetById(id);
            if (job == null || foundJobProject == null)
            {
                return NotFound();
            }

            var viewModel = JobProjectViewModelMapper.MapFrom(id, jobProject);
            var domainModel = JobProjectDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = await _jobProjectRepository.Save(domainModel);

            var updatedViewModel = JobProjectViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobProject(int id)
        {
            var jobProject = await _jobProjectRepository.GetById(id);
            if (jobProject == null)
            {
                return NotFound();
            }

            _jobProjectRepository.Delete(id);
            return NoContent();
        }
    }
}