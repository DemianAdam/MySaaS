using MySaaS.Domain;

namespace MySaaS.Application.Interfaces.Base
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T obj);
        Task<int> RemoveAsync(int objId);
        Task<int> UpdateAsync(T obj);
        Task<T?> GetByIdAsync(int objId);
        Task<bool> ExistsAsync(int objId);
    }
}