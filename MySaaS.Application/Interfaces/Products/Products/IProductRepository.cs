using MySaaS.Application.Interfaces.Base;
using MySaaS.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Products.Products
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
