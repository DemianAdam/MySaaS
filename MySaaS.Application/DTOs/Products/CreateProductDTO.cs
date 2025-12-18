using MySaaS.Application.DTOs.Products.Components;

namespace MySaaS.Application.DTOs.Products
{
    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public int? RecipeId { get; set; }
        public List<CreateProductComponentDTO>? ProductComponents { get; set; }
    }
}
