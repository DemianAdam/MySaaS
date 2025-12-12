namespace MySaaS.Application.Interfaces.Common
{
    public interface IService<Tout,TCreate,TUpdate>
    {
        Task<IEnumerable<Tout>> GetAllAsync();
        Task<Tout> AddAsync(TCreate obj);
        Task RemoveAsync(int objId);
        Task UpdateAsync(TUpdate obj);
    }
}
