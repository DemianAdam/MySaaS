using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
