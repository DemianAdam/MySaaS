using MySaaS.Application.DTOs.Recipes;

namespace MySaaS.Application.DTOs.Items.Products
{
    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public CreateRecipeDTO? Recipe { get; set; }
    }
}
