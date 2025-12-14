using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Inventory;

namespace MySaaS.Domain.Entities.Purchases
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public required int ItemId { get; set; }
        public required int UnitId { get; set; }
        public required int MovementId { get; set; }
        public required int PurchaseId { get; set; }
        public required decimal Quantity { get; set; }
        public required decimal Cost { get; set; }
        public Item? Item { get; set; }
        public Unit? Unit { get; set; }
        public Purchase? Purchase { get; set; }
        public StockMovement? StockMovement { get; set; }
    }
}
