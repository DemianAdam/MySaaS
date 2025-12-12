using MySaaS.Application.DTOs.Common.UnitConversions;
using MySaaS.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Application.Interfaces.Unities
{
    public interface IUnitConversionService : IService<UnitConversionDTO, CreateUnitConversionDTO, UpdateUnitConversionDTO>
    {
    }
}
