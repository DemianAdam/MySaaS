using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Purchases
{
    public class PurchaseResponse : IResponse
    {
        public required int Id { get; init; }
        public required DateTime Date { get; set; }
        public required List<ItemInfoResponse> Items { get; set; }
    }

    public class ItemInfoResponse : IResponse
    {
        public required int Id { get; init; }
        public required int UnitId { get; set; }
        public required decimal Amount { get; set; }
        public required decimal Cost { get; set; }  
    }
}
