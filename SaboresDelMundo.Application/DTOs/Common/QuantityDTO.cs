using MySaaS.Application.DTOs.Unities;

namespace MySaaS.Application.DTOs.Common
{
    public class QuantityDTO
    {
        public required UnitDTO Unit { get; set; }
        public required double Amount { get; set; }
    }
}
