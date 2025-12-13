namespace MySaaS.Application.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWork BeginTransaction();
        void Commit();
        void Rollback();
    }
}
