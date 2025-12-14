using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Inventory;
using MySaaS.Domain.Entities.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class PurchaseItemSQL
    {
        #region Query
        public const string Select =
            $"""
                SELECT
                    purchase_items.id AS {nameof(PurchaseItemModel.Id)},
                    purchase_items.quantity AS {nameof(PurchaseItemModel.Amount)},
                    purchase_items.cost AS {nameof(PurchaseItemModel.Cost)},
                    purchases.id AS {nameof(PurchaseItemModel.PurchaseId)},
                    purchases.date AS {nameof(PurchaseItemModel.PurchaseDate)},
                    items.item_id AS {nameof(PurchaseItemModel.ItemId)},
                    items.name AS {nameof(PurchaseItemModel.ItemName)},
                    items.description AS {nameof(PurchaseItemModel.ItemDescription)},
                    unities.unit_id AS {nameof(PurchaseItemModel.UnitId)},
                    unities.name AS {nameof(PurchaseItemModel.UnitName)},
                    stock_movements.id AS {nameof(PurchaseItemModel.MovementId)},
                FROM purchase_items
                """;
        public const string SelectById =
            $"""
                {Select}
                WHERE purchase_items.id = @Id
            """;
        public const string Exists =
            """
                SELECT EXISTS (
                    SELECT 1 
                    FROM pruchase_items
                    WHERE id = @Id
                );
            """;
        #endregion
        #region Manipulation
        public const string Insert =
            """
                INSERT INTO purchase_items
                    (purchase_id, item_id, unit_id, quantity, cost, movement_id)
                VALUES
                    (@PurchaseId, @ItemId, @UnitId, @Quantity, @Cost, @MovementId)
                RETURNING id
            """;
        public const string Delete =
            """
                DELETE FROM purchase_items
                WHERE id = @Id
            """;
        public const string Update =
            """
                UPDATE purchase_items
                SET
                    purchase_id = @PurchaseId
                    item_id = @ItemId,
                    unit_id = @UnitId,
                    quantity = @Quantity,
                    cost = @Cost,
                    movement_id = @MovementId
                WHERE id = @Id
            """;
        #endregion

    }
}
