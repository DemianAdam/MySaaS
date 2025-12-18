using MySaaS.Application.DTOs.Products.Category;

namespace MySaaS.Application.DTOs.Products.Components
{
    public class ProductComponentDTO
    {
        public required int Id { get; set; }
        public required int CategoryId { get; set; }
        public required CategoryDTO Category { get; set; }
    }
}
