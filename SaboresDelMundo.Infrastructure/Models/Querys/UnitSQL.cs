using MySaaS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models.Querys
{
    internal static class UnitSQL
    {
        #region Query
        public const string Select =
            $"""
                SELECT 
                    unities.unit_id AS {nameof(Unit.Id)},
                    unities.name AS {nameof(Unit.Name)}
                FROM unities
            """;
        public const string SelectById =
            $"""
                {Select}
                WHERE unit_id = @Id
            """;
        public const string Exists =
            """
            SELECT EXISTS (
                SELECT 1
                FROM unities
                WHERE unit_id = @Id
            );
            """;
        #endregion
        #region Manipulation
        public const string Insert =
            """
                INSERT INTO unities 
                    (name)
                VALUES 
                    (@Name)
                RETURNING unit_id;
            """;
        public const string Delete =
            $"""
                DELETE FROM unities
                WHERE unit_id = @Id
            """;
        public const string Update =
            $"""
                UPDATE unities 
                SET
                    name = @Name
                WHERE unit_id = @Id;
            """;
        #endregion
    }
}
