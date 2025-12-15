using MySaaS.Application.DTOs.Common.UnitConversions;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Application.Interfaces.Common.Unities;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Services
{
    public class UnitConversionService : IUnitConversionService
    {
        private readonly IUnitConversionRepository _unitConversionRepository;
        public UnitConversionService(
            IUnitConversionRepository unitConversionRepository)
        {
            _unitConversionRepository = unitConversionRepository;
        }
        public async Task<UnitConversionDTO> AddAsync(CreateUnitConversionDTO obj)
        {
            UnitConversion unitConversion = obj.Map();

            int id = await _unitConversionRepository.AddAsync(unitConversion);
            unitConversion.Id = id;
            return unitConversion.Map();
        }


        public async Task<IEnumerable<UnitConversionDTO>> GetAllAsync()
        {
            IEnumerable<UnitConversion> unitConversions = await _unitConversionRepository.GetAllAsync();
            return unitConversions.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            int affected = await _unitConversionRepository.RemoveAsync(objId);
            if (affected == 0)
            {
                throw new NotFoundException<UnitConversion>(objId);
            }
        }


        public async Task<UnitConversionDTO> UpdateAsync(UpdateUnitConversionDTO obj)
        {
            UnitConversion unitConversion = obj.Map();
            int affected = await _unitConversionRepository.UpdateAsync(unitConversion);
            if (affected == 0)
            {
                throw new NotFoundException<UnitConversion>(obj.Id);
            }
            return unitConversion.Map();
        }
    }
}
