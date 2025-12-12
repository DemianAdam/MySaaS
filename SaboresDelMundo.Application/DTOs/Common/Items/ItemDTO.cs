using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Common.Items
{
    public class ItemDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
