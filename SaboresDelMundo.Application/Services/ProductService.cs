using MySaaS.Application.DTOs.Items.Products;
using MySaaS.Application.Interfaces.Products;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddAsync(CreateProductDTO obj)
        {
            Product product = obj.Map();
            return await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return products.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            int affected = await _productRepository.RemoveAsync(objId);
            if (affected == 0)
            {
                throw new NotFoundException<Product>(objId);
            }
        }

        public async Task UpdateAsync(UpdateProductDTO obj)
        {
            Product product = obj.Map();
            int affected = await _productRepository.UpdateAsync(product);
            if (affected == 0)
            {
                throw new NotFoundException<Product>(obj.Id);
            }
        }
    }
}
