using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.DTOs.Products
{
    public class ProductResponse : IResponse
    {
        public required int Id { get; init; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public List<int>? CategoriesId { get; set; }
    }
}
