using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Purchases
{
    public class CreatePurchaseDTO
    {
        public required List<ItemInfoDTO> Items { get; set; }
    }
}
