using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Web.ActionFilters;
using Web.Models.JobModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public JobController(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAll();
            var jobViews = _mapper.Map<IEnumerable<JobViewModel>>(jobs);
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

            var jobViewModel = _mapper.Map<JobViewModel>(job);
            return Ok(jobViewModel);
        }

        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> AddJob([FromBody] AddJobViewModel entity)
        {
            var domainModel = _mapper.Map<JobDomainModel>(entity);
            var savedJob = await _jobRepository.Save(domainModel);
            var jobViewModel = _mapper.Map<JobViewModel>(savedJob);

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

            var viewModel = _mapper.Map<JobViewModel>(entity);
            var domainModel = _mapper.Map<JobDomainModel>(viewModel);
            var updatedDomainModel = await _jobRepository.Save(domainModel);

            var updatedViewModel = _mapper.Map<JobViewModel>(updatedDomainModel);
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