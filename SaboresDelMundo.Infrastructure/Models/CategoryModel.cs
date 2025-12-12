using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class CategoryModel
    {
        public required int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
