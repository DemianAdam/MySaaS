using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Inventory;
using MySaaS.Domain.Entities.Purchases;
using MySaaS.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Mappers
{
    internal static class PurchaseItemMapper
    {
        public static PurchaseItem Map(this PurchaseItemModel purchaseItemModel)
        {
            Item item = new Item()
            {
                Id = purchaseItemModel.ItemId,
                Name = purchaseItemModel.ItemName,
                Description = purchaseItemModel.ItemDescription,
            };

            Unit unit = new Unit()
            {
                Id = purchaseItemModel.UnitId,
                Name = purchaseItemModel.UnitName,
            };

            Purchase purchase = new Purchase()
            {
                Id = purchaseItemModel.PurchaseId,
                Date = purchaseItemModel.PurchaseDate
            };

            //TODO: maybe map?
            /* StockMovement stockMovement = new StockMovement()
             {
                 Id = purchaseItemModel.MovementId,
             };*/
            return new PurchaseItem()
            {
                Id = purchaseItemModel.Id,
                Item = item,
                ItemId = item.Id,
                Cost = purchaseItemModel.Cost,
                Purchase = purchase,
                PurchaseId = purchase.Id,
                MovementId = purchaseItemModel.MovementId,
                Quantity = purchaseItemModel.Amount,
                Unit = unit,
                UnitId = unit.Id
            };
        }

        public static IEnumerable<PurchaseItem> Map(this IEnumerable<PurchaseItemModel> purchaseItemModels)
        {
            return purchaseItemModels.Select(Map);
        }
    }
}
