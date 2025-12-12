using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
