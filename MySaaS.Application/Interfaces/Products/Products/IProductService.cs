using MySaaS.Application.DTOs.Products;
using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Products.Products
{
    public interface IProductService : IService<ProductDTO, CreateProductDTO, UpdateProductDTO,ProductResponse>
    {
    }
}
