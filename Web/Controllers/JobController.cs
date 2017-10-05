using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces;
using Web.ActionFilters;
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
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAll();
            var jobViews = JobViewModelMapper.MapFrom(jobs); ;
            return Ok(jobViews);
        }

        [HttpGet("{id}", Name = "GetJob")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }

            var jobViewModel = JobViewModelMapper.MapFrom(job);
            return Ok(jobViewModel);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddJob([FromBody] AddJobViewModel entity)
        {
            var domainModel = JobDomainModelMapper.MapFrom(entity);
            var savedJob = await _jobRepository.Save(domainModel);
            var jobViewModel = JobViewModelMapper.MapFrom(savedJob);

            return CreatedAtRoute("GetJob", new { id = jobViewModel.Id }, jobViewModel);
        }

        [HttpPut("{id}")]
        [ModelStateValidation]
        [ModelHasCorrectId]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] UpdateJobViewModel entity)
        {
            var foundJob = await _jobRepository.GetById(id);
            if (foundJob == null)
            {
                return NotFound();
            }

            var viewModel = JobViewModelMapper.MapFrom(id, entity);
            var domainModel = JobDomainModelMapper.MapFrom(viewModel);
            var updatedDomainModel = await _jobRepository.Save(domainModel);

            var updatedViewModel = JobViewModelMapper.MapFrom(updatedDomainModel);
            return Ok(updatedViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobRepository.GetById(id);
            if (job != null)
            {
                _jobRepository.Delete(id);
            }
            return NoContent();
        }
    }
}