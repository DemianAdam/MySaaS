using MySaaS.Application.DTOs.Common.UnitConversions;
using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.UnitConversion
{
    public class UnitConversionResponse : IResponse
    {
        public required int Id { get; init; }
        public required int ItemId { get; set; }
        public required int FromUnitId { get; set; }
        public required int ToUnitId { get; set; }
        public required double ConversionFactor { get; set; }

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
