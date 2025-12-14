using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.UnitConversions
{
    public class UpdateUnitConversionDTO
    {
        public required int Id { get; set; }
        public required int ItemId { get; set; }
        public required int FromUnitId { get; set; }
        public required int ToUnitId { get; set; }
        public required double ConversionFactor { get; set; }
    }
}
