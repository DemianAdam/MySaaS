

namespace MySaaS.Application.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWork BeginTransaction();
        void Commit();
        void Rollback();
    }
}
