using MySaaS.Application.DTOs.Items.Products;
using MySaaS.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Products
{
    public interface IProductService : IService<ProductDTO, CreateProductDTO, UpdateProductDTO>
    {
    }
}
