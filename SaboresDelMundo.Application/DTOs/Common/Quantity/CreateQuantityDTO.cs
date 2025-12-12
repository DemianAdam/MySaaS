namespace MySaaS.Application.DTOs.Common.Quantity
{
    public class CreateQuantityDTO
    {
        public required int UnitId { get; set; }
        public required double Amount { get; set; }
    }
}
