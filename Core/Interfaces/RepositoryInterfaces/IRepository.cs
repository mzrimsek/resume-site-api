using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Save(T entity);
        Task Delete(int id);
    }
}