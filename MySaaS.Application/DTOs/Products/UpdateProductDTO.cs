using MySaaS.Application.DTOs.Products.Category;

namespace MySaaS.Application.DTOs.Products
{
    public class UpdateProductDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public int? RecipeId { get; set; }
        public List<int>? IdCategories { get; set; }
    }
}
