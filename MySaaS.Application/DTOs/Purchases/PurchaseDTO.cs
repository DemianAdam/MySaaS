using MySaaS.Domain.Entities.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Purchases
{
    public class PurchaseDTO
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required List<PurchaseItem> Items { get; set; }
    }
}
