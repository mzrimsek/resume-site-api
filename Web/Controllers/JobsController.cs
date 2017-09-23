using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Mappers.JobMappers;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private readonly IJobRepository _jobRepository;
        public JobsController(IJobRepository jobRepository)
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

        [HttpGet("{id}")]
        public IActionResult GetJob(int id)
        {
            var job = _jobRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public IActionResult AddJob(JobViewModel job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var domainModel = JobDomainModelMapper.MapFrom(job);
            var savedJob = _jobRepository.Save(domainModel);

            return CreatedAtRoute($"/{savedJob.Id}", savedJob);
        }
    }
}