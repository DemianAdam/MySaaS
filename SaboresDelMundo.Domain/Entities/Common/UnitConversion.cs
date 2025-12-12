using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Domain.Entities.Common
{
    public class UnitConversion
    {
        public int Id { get; set; }
        public required int ItemId { get; set; }
        public required int FromUnitId { get; set; }
        public required int ToUnitId { get; set; }
        public required double ConversionFactor { get; set; }

        public UnitConversion Reverse
        {
            get => new UnitConversion
            {
                Id = this.Id,
                ItemId = this.ItemId,
                FromUnitId = this.ToUnitId,
                ToUnitId = this.FromUnitId,
                ConversionFactor = 1 / this.ConversionFactor
            };
        }
    }
}
