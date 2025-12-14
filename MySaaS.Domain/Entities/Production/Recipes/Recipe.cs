using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Exceptions.Recipe;

namespace MySaaS.Domain.Entities.Production.Recipes
{
    public class Recipe
    {
        public required int Id { get; set; }
        public Item? Item { get; set; }
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
            //TODO: Validar.
            _recipeComponents.Add(component);
        }
    }
}
