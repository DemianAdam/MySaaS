using MySaaS.Domain.Entities.Recipes;

namespace MySaaS.Domain.Entities
{
    public class Ingredient
    {
        public required int SupplyId { get; set; }
        public Supply? Supply { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
