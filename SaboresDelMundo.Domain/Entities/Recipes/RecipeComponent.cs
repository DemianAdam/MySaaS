namespace MySaaS.Domain.Entities.Recipes
{
    public class RecipeComponent
    {
        public required Ingredient Ingredient { get; set; }

        public required Quantity Weight { get; set; }

        public required Quantity Waste { get; set; }
    }
}
