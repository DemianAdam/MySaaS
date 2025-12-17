using MySaaS.Application.DTOs.Common.UnitConversion;
using MySaaS.Application.DTOs.Common.UnitConversions;
using MySaaS.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Common.Unities
{
    //TODO: Change last type parameter for UnitConversionResponse when implemented
    public interface IUnitConversionService : IService<UnitConversionDTO, CreateUnitConversionDTO, UpdateUnitConversionDTO, UnitConversionResponse>
    {
    }
}
