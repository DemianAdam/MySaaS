using MySaaS.Application.DTOs.Unities;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Interfaces.Unities
{
    public interface IUnitService : IService<UnitDTO,CreateUnitDTO,UpdateUnitDTO>
    {
    }
}
