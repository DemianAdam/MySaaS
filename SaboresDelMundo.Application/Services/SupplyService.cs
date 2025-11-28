using MySaaS.Application.DTOs.Supplies;
using MySaaS.Application.Interfaces.Supplies;
using MySaaS.Application.Interfaces.Supplies.Ingredients;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Exceptions.Common;
using MySaaS.Domain.Exceptions.Supply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Services
{
    internal class SupplyService : ISupplyService
    {
        private readonly ISupplyRepository _supplyRepository;
        private readonly IIngredientRepository _ingredientRepository;
        public SupplyService(ISupplyRepository supplyRepository, IIngredientRepository ingredientRepository)
        {
            _supplyRepository = supplyRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<int> AddAsync(SupplyDTO obj)
        {
            Supply supply = obj.Map();
            return await _supplyRepository.AddAsync(supply);
        }

        public async Task<IEnumerable<SupplyDTO>> GetAllAsync()
        {
            IEnumerable<Supply> supplies = await _supplyRepository.GetAllAsync();
            return supplies.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            bool exists = await _supplyRepository.ExistsAsync(objId);
            if (!exists)
            {
                throw new NotFoundException<Supply>(objId);
            }

            bool isUsed = await _ingredientRepository.ExistsAsync(objId);
            if (isUsed)
            {
                throw new SupplyInUseByIngredientException();
            }
            await _supplyRepository.RemoveAsync(objId);
        }

        public async Task UpdateAsync(SupplyDTO obj)
        {
            Supply supply = obj.Map();
            int affected = await _supplyRepository.UpdateAsync(supply);
            if(affected == 0)
            {
                throw new NotFoundException<Supply>(obj.Id);
            }
        }
    }
}
