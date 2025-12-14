using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.UnitConversions
{
    public class UnitConversionDTO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int FromUnitId { get; set; }
        public int ToUnitId { get; set; }
        public double ConversionFactor { get; set; }

        public UnitConversionDTO Reverse
        {
            get => new UnitConversionDTO
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
