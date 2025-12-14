using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class RecipeModel
    {
        public required int ItemId { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public required decimal RecipeAmount { get; set; }
        public required int UnitId { get; set; }
        public required string UnitName { get; set; }
    }
}
