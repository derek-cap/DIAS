using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIAS
{
    public interface IRepo<T> : IRepository where T : IAggregateRoot
    {
        int Save(T entity);
        Task<int> SaveAsync(T entity);

        int Delete(int id);
        Task<int> DeleteAsync(int id);

        int Delete(T entity);
        Task<int> DeleteAsync(T entity);

        T GetOne(int? id);
        Task<T> GetOneAsync(int? id);

        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
    }
}
