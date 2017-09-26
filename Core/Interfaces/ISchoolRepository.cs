using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ISchoolRepository
    {
        IEnumerable<SchoolDomainModel> GetAll();
        SchoolDomainModel GetById(int id);
        SchoolDomainModel Save(SchoolDomainModel school);
        void Delete(int id);
        SchoolDomainModel Update(SchoolDomainModel school);
    }
}