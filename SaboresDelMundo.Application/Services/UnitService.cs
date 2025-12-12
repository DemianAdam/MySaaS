using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.Interfaces.Unities;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Services
{
    internal class UnitService : IUnitService
    {
        private readonly IUnitRepository _unityRepository;

        public UnitService(IUnitRepository unityRepository)
        {
            _unityRepository = unityRepository;
        }

        public async Task<int> AddAsync(CreateUnitDTO unity)
        {
            Unit entity = unity.Map();
            return await _unityRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<UnitDTO>> GetAllAsync()
        {
            IEnumerable<Unit> entities = await _unityRepository.GetAllAsync();
            return entities.Map();
        }

        public async Task RemoveAsync(int unityId)
        {
            int affected = await _unityRepository.RemoveAsync(unityId);
            if (affected == 0)
            {
                throw new NotFoundException<Unit>(unityId);
            }
        }

        public async Task UpdateAsync(UpdateUnitDTO unity)
        {
            Unit entity = unity.Map();
            int affected = await _unityRepository.UpdateAsync(entity);
            if (affected == 0)
            {
                throw new NotFoundException<Unit>(unity.Id);
            }
        }
    }
}
