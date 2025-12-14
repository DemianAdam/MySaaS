using MySaaS.Application.Interfaces.Base;
using MySaaS.Domain.Entities.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Purchases.PurchaseItems
{
    public interface IPurchaseItemRepository : IRepository<PurchaseItem>
    {
    }
}
