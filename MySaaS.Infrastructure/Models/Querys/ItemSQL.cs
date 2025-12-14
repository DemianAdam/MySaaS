using MySaaS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class ItemSQL
    {
        #region Query

        public const string Select =
            $"""
                SELECT 
                    items.item_id AS {nameof(Item.Id)},
                    items.name AS {nameof(Item.Name)},
                    items.description AS {nameof(Item.Description)}
                FROM items
            """;
        public const string SelectById =
            $"""
                {Select}
                WHERE item_id = @Id;
            """;
        public const string Exists =
            $"""
                SELECT EXISTS (      
                    SELECT 1
                    FROM items
                    WHERE item_id = @Id
                )
            """;
        #endregion
        #region Manipulation
        public const string Insert =
            $"""
                INSERT INTO items
                    (name,description)
                VALUES
                    (@Name,@Description)
                RETURNING item_id
            """;

        public const string Delete =
            $"""
                DELETE FROM items
                WHERE item_id = @Id;
            """;

        public const string Update =
            $"""
                UPDATE items
                SET
                    name = @Name,
                    description = @Description
                WHERE item_id = @Id;
            """;
        #endregion
    }
}
