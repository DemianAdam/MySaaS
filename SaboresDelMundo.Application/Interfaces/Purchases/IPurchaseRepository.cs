using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain.Entities.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Purchases
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
    }
}
