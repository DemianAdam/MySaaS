using MySaaS.Application.DTOs.Production.Recipes.Relations;
using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Production.Recipes
{
    public class RecipeResponse : IResponse
    {
        public required int Id { get; init; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required List<RecipeRelationsResponse> Ingredients { get; set; }
        public required int UnitId { get; set; }
        public required decimal Amount { get; set; }
    }
}
