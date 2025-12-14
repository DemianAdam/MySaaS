using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.Interfaces.Base;
using MySaaS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Interfaces.Common.Unities
{
    public interface IUnitService : IService<UnitDTO,CreateUnitDTO,UpdateUnitDTO>
    {
    }
}
