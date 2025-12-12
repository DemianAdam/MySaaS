using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Domain.Entities.Purchases
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<PurchaseItem> Items { get; set; } = new();
    }
}
