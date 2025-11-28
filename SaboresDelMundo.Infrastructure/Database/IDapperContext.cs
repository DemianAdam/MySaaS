using System.Data;

namespace MySaaS.Infrastructure.Database
{
    internal interface IDapperContext
    {
        IDbConnection Connection { get; }
        IDbTransaction? Transaction { get; }
    }
}
