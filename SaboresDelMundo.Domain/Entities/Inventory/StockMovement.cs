using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Domain.Entities.Inventory
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public MovementType MovementType { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum MovementType
    {
        IN,
        OUT
    }
}
