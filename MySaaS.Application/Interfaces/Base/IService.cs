namespace MySaaS.Application.Interfaces.Base
{
    public interface IService<Tout,TCreate,TUpdate,TResponse> where TResponse : IResponse
    {
        Task<IEnumerable<Tout>> GetAllAsync();
        Task<TResponse> AddAsync(TCreate obj);
        Task RemoveAsync(int objId);
        Task<TResponse> UpdateAsync(TUpdate obj);
    }
}
