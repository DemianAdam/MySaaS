using MySaaS.Application.DTOs.Common.Quantity;

namespace MySaaS.Application.DTOs.Purchases
{
    public class ItemInfoDTO
    {
        public required int ItemId { get; set; }
        public required CreateQuantityDTO Quantity { get; set; }
        public required decimal Cost { get; set; }
    }
}
