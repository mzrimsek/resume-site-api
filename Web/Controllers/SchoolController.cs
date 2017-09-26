using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Web.Mappers.JobMappers;
using Web.Models;

namespace Web.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }
    }
}