using MySaaS.Application.DTOs.Common.Quantity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Production.Recipes.Components
{
    public class CreateRecipeRelationsDTO
    {
        public required List<CreateRecipeComponentDTO> Ingredients { get; set; }
        public required CreateQuantityDTO Quantity { get; set; }
    }
}
