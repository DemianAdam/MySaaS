using MySaaS.Domain.Exceptions.Recipe;

namespace MySaaS.Domain.Entities.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        private List<RecipeComponent> _recipeComponents = new();
        public IReadOnlyCollection<RecipeComponent> Ingredients => _recipeComponents.AsReadOnly();
        public required Quantity Quantity { get; set; }
        public Recipe(IEnumerable<RecipeComponent> ingredients)
        {
            UpdateComponents(ingredients);
        }

        public Recipe()
        {
        }

        public void UpdateComponents(IEnumerable<RecipeComponent> components)
        {
            if (!components.Any())
            {
                throw new RecipeWithoutComponentsException();
            }

            _recipeComponents.Clear();

            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        public void AddComponent(RecipeComponent component)
        {
            //UNDONDE: Validar.
            _recipeComponents.Add(component);
        }
    }
}
