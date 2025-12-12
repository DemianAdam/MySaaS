using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class ProductCategoryModel
    {
        public required int Id { get; set; }
        public required int ProductId { get; set; }
        public required int CategoryId { get; set; }
        public required string CategoryName { get; set; }

    }
}
