using MySaaS.Application.DTOs.Products;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Application.Interfaces.Items;
using MySaaS.Application.Interfaces.Products;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities.Products;
using MySaaS.Domain.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(
            IItemRepository itemRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(CreateProductDTO obj)
        {
            Product product = obj.Map();
            if (product.Item is null)
            {
                throw new ArgumentException("Product must have an associated Item.");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                int itemId = await _itemRepository.AddAsync(product.Item);
                product.ItemId = itemId;
                int productId = await _productRepository.AddAsync(product);
                _unitOfWork.Commit();
                return productId;
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return products.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                int affected = await _productRepository.RemoveAsync(objId);
                if (affected == 0)
                {
                    throw new NotFoundException<Product>(objId);
                }
                await _itemRepository.RemoveAsync(objId);
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task UpdateAsync(UpdateProductDTO obj)
        {
            Product product = obj.Map();
            if (product.Item is null)
            {
                throw new ArgumentException("Product must have an associated Item.");
            }
            _unitOfWork.BeginTransaction();
            try
            {
                int affected = await _productRepository.UpdateAsync(product);
                if (affected == 0)
                {
                    throw new NotFoundException<Product>(product.ItemId);
                }
                await _itemRepository.UpdateAsync(product.Item);
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
