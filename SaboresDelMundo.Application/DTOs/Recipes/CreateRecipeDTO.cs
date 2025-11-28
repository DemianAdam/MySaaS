using MySaaS.Application.DTOs.Common;
using MySaaS.Application.DTOs.Recipes.Components;
using MySaaS.Application.DTOs.Unities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Recipes
{
    public class CreateRecipeDTO
    {
        public required string Name { get; set; }
        public required List<CreateRecipeComponentDTO> Ingredients { get; set; }
        public required CreateQuantityDTO Quantity { get; set; }
    }
}
