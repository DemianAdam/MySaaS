using MySaaS.Application.DTOs.Common.Unit;
using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Mappers
{
    internal static class UnitMapper
    {
        public static UnitDTO Map(this Unit unity)
        {
            return new UnitDTO
            {
                Id = unity.Id,
                Name = unity.Name
            };
        }

        public static Unit Map(this UnitDTO unity)
        {
            return new Unit
            {
                Id = unity.Id,
                Name = unity.Name
            };
        }

        public static Unit Map(this UpdateUnitDTO updateUnitDTO)
        {
            return new Unit
            {
                Id = updateUnitDTO.Id,
                Name = updateUnitDTO.Name
            };
        }

        public static Unit Map(this CreateUnitDTO createUnitDTO)
        {
            return new Unit
            {
                Name = createUnitDTO.Name
            };
        }

        public static UnitResponse ToResponse(this Unit unit)
        {
            return new UnitResponse
            {
                Id = unit.Id,
                Name = unit.Name
            };
        }
        public static IEnumerable<UnitDTO> Map(this IEnumerable<Unit> unities)
        {
            return unities.Select(x => x.Map());
        }

        public static IEnumerable<Unit> Map(this IEnumerable<UnitDTO> unities)
        {
            return unities.Select(x => x.Map());
        }
    }
}
