using MySaaS.Application.DTOs.Common.Unities;

namespace MySaaS.Application.DTOs.Common.Quantity
{
    public class QuantityDTO
    {
        public required UnitDTO Unit { get; set; }
        public required decimal Amount { get; set; }
    }
}
