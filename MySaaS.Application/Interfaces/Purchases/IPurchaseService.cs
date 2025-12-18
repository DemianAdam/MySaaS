using MySaaS.Application.DTOs.Products;
using MySaaS.Application.DTOs.Purchases;
using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Purchases
{
    //TODO: fix last type parameter
    public interface IPurchaseService : IService<PurchaseDTO,CreatePurchaseDTO, UpdateProductDTO,ProductResponse>
    {
    }
}
