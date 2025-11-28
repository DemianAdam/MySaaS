using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class RecipeModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; } 
        public required double Amount { get; set; }
        public required int UnitId { get; set; }
        public required string UnitName { get; set; }
    }
}
