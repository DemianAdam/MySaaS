using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Production.Ingredients
{
    public class IngredientResponse : IResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
