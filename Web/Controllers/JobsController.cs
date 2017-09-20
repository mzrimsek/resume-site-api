using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Mappers.JobMappers;

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
    }
}