using MySaaS.Domain.Entities.Recipes;

namespace MySaaS.Domain.Entities
{
    public class Product
    {
        public required int ItemId { get; set; }
        public Item? Item { get; set; }
        public required decimal Price { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
