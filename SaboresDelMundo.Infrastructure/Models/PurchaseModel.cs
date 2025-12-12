using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Infrastructure.Models
{
    internal class PurchaseModel
    {
        public required int PurchaseId { get; set; }
        public required DateTime PurchaseDate { get; set; }
    }
}
