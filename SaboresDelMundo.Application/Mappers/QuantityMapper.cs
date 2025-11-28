using MySaaS.Application.DTOs.Common;
using MySaaS.Domain.Entities;

namespace MySaaS.Application.Mappers
{
    internal static class QuantityMapper
    {
        public static QuantityDTO Map(this Quantity quantity)
        {
            if (quantity.Unit == null)
            {
                throw new InvalidOperationException("Quantity's Unit property is null.");
            }

            return new QuantityDTO
            {
                Amount = quantity.Amount,
                Unit = quantity.Unit.Map()
            };
        }

        public static Quantity Map(this QuantityDTO quantityDTO)
        {
            return new Quantity
            {
                Amount = quantityDTO.Amount,
                Unit = quantityDTO.Unit.Map(),
                UnitId = quantityDTO.Unit.Id
            };
        }

        public static Quantity Map(this CreateQuantityDTO createQuantityDTO)
        {
            return new Quantity
            {
                Amount = createQuantityDTO.Amount,
                UnitId = createQuantityDTO.UnitId,
            };
        }

        public static IEnumerable<QuantityDTO> Map(this IEnumerable<Quantity> quantities)
        {
            return quantities.Select(quantity => quantity.Map());
        }

        public static IEnumerable<Quantity> Map(this IEnumerable<QuantityDTO> quantityDTOs)
        {
            return quantityDTOs.Select(quantityDTO => quantityDTO.Map());
        }
    }
}
