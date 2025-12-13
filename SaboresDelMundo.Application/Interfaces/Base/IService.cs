namespace MySaaS.Application.Interfaces.Base
{
    public interface IService<Tout,TCreate,TUpdate>
    {
        Task<IEnumerable<Tout>> GetAllAsync();
        Task<Tout> AddAsync(TCreate obj);
        Task RemoveAsync(int objId);
        Task UpdateAsync(TUpdate obj);
    }
}
