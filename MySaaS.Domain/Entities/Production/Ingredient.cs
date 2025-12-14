using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;

namespace MySaaS.Domain.Entities.Production
{
    public class Ingredient
    {
        public required int ItemId { get; set; }
        public Item? Item { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
