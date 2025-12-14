using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Purchases
{
    public class UpdatePurchaseDTO
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required List<ItemInfoDTO> Items { get; set; }
    }
}
