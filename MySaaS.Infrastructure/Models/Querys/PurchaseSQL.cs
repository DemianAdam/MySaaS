using MySaaS.Domain.Entities.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class PurchaseSQL
    {
        #region Query
        public const string Select =
            $"""
                SELECT
                    id AS {nameof(PurchaseModel.PurchaseId)},
                    date AS {nameof(PurchaseModel.PurchaseDate)}
                FROM purchases
            """;
        public const string SelectById =
            $"""
                {Select}
                WHERE id = @Id
            """;
        public const string Exists =
            """
                SELECT EXISTS(
                    SELECT 1
                    FROM purchases
                    WHERE id = @Id
                )
            """;
        #endregion
        #region Manipulation
        public const string Insert =
            """
                INSERT INTO purchases
                    (date)
                VALUES
                    (@Date)
                RETURNING id
            """;
        public const string Delete =
            """
                DELETE FROM purchases
                WHERE id = @Id
            """;
        public const string Update =
            """
                UPDATE purchases
                SET
                    date = @Date,
                WHERE id = @Id
            """;
        #endregion
    }
}
