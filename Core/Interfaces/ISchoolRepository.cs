using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
  public interface ISchoolRepository
  {
    IEnumerable<School> GetAll();
    School GetById(int id);
  }
}