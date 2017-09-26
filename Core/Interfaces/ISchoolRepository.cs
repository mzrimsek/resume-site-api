using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ISchoolRepository
    {
        IEnumerable<SchoolDomainModel> GetAll();
        SchoolDomainModel GetById(int id);
        SchoolDomainModel Save(SchoolDomainModel school);
        SchoolDomainModel Delete(int id);
    }
}