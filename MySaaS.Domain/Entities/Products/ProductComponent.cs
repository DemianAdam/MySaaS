using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Domain.Entities.Products
{
    public class ProductComponent
    {
        public int Id { get; set; }
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
