using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.Unit
{
    public class UnitResponse : IResponse
    {
        public required int Id { get; init; }
        public required string Name { get; set; }
    }
}
