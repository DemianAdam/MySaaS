using MySaaS.Domain.Entities.Recipes;

namespace MySaaS.Domain.Entities
{
    public class Ingredient
    {
        public required int ItemId { get; set; }
        public Item? Item { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
