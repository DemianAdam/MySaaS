using MySaaS.Domain.Entities.Recipes;

namespace MySaaS.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
