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
        private List<Category> _categories = new();
        public IReadOnlyCollection<Category>? Categories => _categories;

        public Product(IEnumerable<Category>? categories)
        {
            UpdateCategories(categories);
        }
        public Product()
        {
            
        }

        public void UpdateCategories(IEnumerable<Category>? categories)
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

        public void AddCategory(Category category)
        {
            //TODO: Validate
            _categories.Add(category);
        }
    }
}
