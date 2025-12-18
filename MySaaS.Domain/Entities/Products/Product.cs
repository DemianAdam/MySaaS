using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;

namespace MySaaS.Domain.Entities.Products
{
    public class Product
    {
        public required int ItemId { get; set; }
        public Item? Item { get; set; }
        public required decimal Price { get; set; }
        public int? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        private List<ProductComponent> _components = new();
        public IReadOnlyCollection<ProductComponent>? ProductComponents => _components;

        public Product(IEnumerable<ProductComponent>? categories)
        {
            UpdateCategories(categories);
        }
        public Product()
        {
            
        }

        public void UpdateCategories(IEnumerable<ProductComponent>? categories)
        {
            if(categories is null)
            {
                return;
            }
            foreach (var item in categories)
            {
                AddCategory(item);
            }
        }

        public void AddCategory(ProductComponent category)
        {
            //TODO: Validate
            _components.Add(category);
        }
    }
}
