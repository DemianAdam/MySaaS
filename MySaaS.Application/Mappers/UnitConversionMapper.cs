using MySaaS.Application.DTOs.Common.UnitConversions;
using MySaaS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Mappers
{
    public static class UnitConversionMapper
    {
        public static UnitConversionDTO Map(this UnitConversion unitConversion)
        {
            return new UnitConversionDTO
            {
                Id = unitConversion.Id,
                ItemId = unitConversion.ItemId,
                FromUnitId = unitConversion.FromUnitId,
                ToUnitId = unitConversion.ToUnitId,
                ConversionFactor = unitConversion.ConversionFactor
            };
        }

        public static UnitConversion Map(this UnitConversionDTO unitConversionDTO)
        {
            return new UnitConversion
            {
                Id = unitConversionDTO.Id,
                ItemId = unitConversionDTO.ItemId,
                FromUnitId = unitConversionDTO.FromUnitId,
                ToUnitId = unitConversionDTO.ToUnitId,
                ConversionFactor = unitConversionDTO.ConversionFactor
            };
        }

        public static UnitConversion Map(this CreateUnitConversionDTO createUnitConversionDTO)
        {
            return new UnitConversion
            {
                ItemId = createUnitConversionDTO.ItemId,
                FromUnitId = createUnitConversionDTO.FromUnitId,
                ToUnitId = createUnitConversionDTO.ToUnitId,
                ConversionFactor = createUnitConversionDTO.ToQuantity / createUnitConversionDTO.FromQuantity
            };
        }

        public static UnitConversionDTO ToDTO(this CreateUnitConversionDTO createUnitConversionDTO)
        {
            return new UnitConversionDTO
            {
                ItemId = createUnitConversionDTO.ItemId,
                FromUnitId = createUnitConversionDTO.FromUnitId,
                ToUnitId = createUnitConversionDTO.ToUnitId,
                ConversionFactor = createUnitConversionDTO.ToQuantity / createUnitConversionDTO.FromQuantity
            };
        }

        public static UnitConversion Map(this UpdateUnitConversionDTO updateUnitConversionDTO)
        {
            return new UnitConversion
            {
                Id = updateUnitConversionDTO.Id,
                ItemId = updateUnitConversionDTO.ItemId,
                FromUnitId = updateUnitConversionDTO.FromUnitId,
                ToUnitId = updateUnitConversionDTO.ToUnitId,
                ConversionFactor = updateUnitConversionDTO.ConversionFactor
            };
        }

        public static IEnumerable<UnitConversionDTO> Map(this IEnumerable<UnitConversion> unitConversions)
        {
            return unitConversions.Select(uc => uc.Map());
        }

        public static IEnumerable<UnitConversion> Map(this IEnumerable<UnitConversionDTO> unitConversionDTOs)
        {
            return unitConversionDTOs.Select(ucdto => ucdto.Map());
        }
    }
}
