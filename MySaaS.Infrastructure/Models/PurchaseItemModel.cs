using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class PurchaseItemModel
    {
        public required int Id { get; set; }
        public required decimal Amount { get; set; }
        public required decimal Cost { get; set; }
        //Purchase
        public required int PurchaseId { get; set; }
        public required DateTime PurchaseDate { get; set; }

        //Item
        public required int ItemId { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }

        //Unit
        public required int UnitId { get; set; }
        public required string UnitName { get; set; }
        

        //Movement
        //TODO: add movement? if add, modify sql
        public int MovementId { get; set; }

    }
}
