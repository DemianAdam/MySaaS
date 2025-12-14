using MySaaS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class UnitConversionSQL
    {
        #region Manipulation
        public const string Insert =
            """
                INSERT INTO unit_conversions
                    (id_item, from_unit_id, to_unit_id, factor)
                VALUES
                    (@ItemId, @FromUnitId, @ToUnitId, @ConversionFactor)
                RETURNING id;
            """;
        public const string Delete=
            """
                DELETE FROM unit_conversions
                WHERE id = @Id;
            """;
        public const string Update=
            """
                UPDATE unit_conversions
                SET
                    id_item = @ItemId,
                    from_unit_id = @FromUnitId,
                    to_unit_id = @ToUnitId,
                    factor = @ConversionFactor
                WHERE id = @Id;
            """;
        #endregion
        #region Query
        public const string Select =
            $"""
                SELECT
                    unit_conversions.id AS {nameof(UnitConversion.Id)},
                    unit_conversions.id_item AS {nameof(UnitConversion.ItemId)},
                    unit_conversions.from_unit_id AS {nameof(UnitConversion.FromUnitId)},
                    unit_conversions.to_unit_id AS {nameof(UnitConversion.ToUnitId)},
                    unit_conversions.factor AS {nameof(UnitConversion.ConversionFactor)}
                FROM unit_conversions
            """;
        public const string SelectById =
            $"""
                {Select}
                WHERE id = @Id
            """;
        public const string Exists =
            """
                SELECT EXISTS (
                    SELECT 1
                    FROM unit_conversions
                    WHERE id = @Id
                );
            """;
        #endregion
    }
}
