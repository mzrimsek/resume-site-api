using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Web.Mappers.JobMappers;
using Web.Models.JobModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            var jobs = _jobRepository.GetAll();
            var jobViews = JobViewModelMapper.MapFrom(jobs); ;
            return Ok(jobViews);
        }

        [HttpGet("{id}", Name = "GetJob")]
        public IActionResult GetJob(int id)
        {
            var job = _jobRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }

            var jobViewModel = JobViewModelMapper.MapFrom(job);
            return Ok(jobViewModel);
        }

        [HttpPost]
        public IActionResult AddJob([FromBody] AddUpdateJobViewModel job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var domainModel = JobDomainModelMapper.MapFrom(job);
            var savedJob = _jobRepository.Save(domainModel);
            var jobViewModel = JobViewModelMapper.MapFrom(savedJob);

            return CreatedAtRoute("GetJob", new { id = jobViewModel.Id }, jobViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob(int id, [FromBody] AddUpdateJobViewModel job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var foundJob = _jobRepository.GetById(id);
            if (foundJob == null)
            {
                return NotFound();
            }

            var viewModel = JobViewModelMapper.MapFrom(id, job);
            var domainModel = JobDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = _jobRepository.Save(domainModel);

            var updatedViewModel = JobViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            var job = _jobRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }

            _jobRepository.Delete(id);
            return NoContent();
        }
    }
}