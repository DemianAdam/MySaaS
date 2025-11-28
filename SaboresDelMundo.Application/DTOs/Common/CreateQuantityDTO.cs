using MySaaS.Application.DTOs.Unities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common
{
    public class CreateQuantityDTO
    {
        public required int UnitId { get; set; }
        public required double Amount { get; set; }
    }
}
