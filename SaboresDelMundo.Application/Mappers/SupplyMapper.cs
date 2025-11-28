using MySaaS.Application.DTOs.Supplies;
using MySaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Mappers
{
    internal static class SupplyMapper
    {
        public static SupplyDTO Map(this Supply supply)
        {
            return new SupplyDTO
            {
                Id = supply.Id,
                Name = supply.Name,
                Description = supply.Description
            };
        }

        public static Supply Map(this SupplyDTO supplyDTO)
        {
            return new Supply
            {
                Id = supplyDTO.Id,
                Name = supplyDTO.Name,
                Description = supplyDTO.Description
            };
        }

        public static IEnumerable<SupplyDTO> Map(this IEnumerable<Supply> supplies)
        {
            return supplies.Select(supply => supply.Map());
        }

        public static IEnumerable<Supply> Map(this IEnumerable<SupplyDTO> supplyDTOs)
        {
            return supplyDTOs.Select(supplyDTO => supplyDTO.Map());
        }
    }
}
