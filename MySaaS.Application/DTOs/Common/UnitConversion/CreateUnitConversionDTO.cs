using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.UnitConversions
{
    public class CreateUnitConversionDTO
    {
        public required int ItemId { get; set; }
        public required int FromUnitId { get; set; }
        public required double FromQuantity { get; set; }
        public required int ToUnitId { get; set; }
        public required double ToQuantity { get; set; }
    }
}
