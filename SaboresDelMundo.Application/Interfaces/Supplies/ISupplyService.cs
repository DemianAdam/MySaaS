using MySaaS.Application.DTOs.Supplies;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain;

namespace MySaaS.Application.Interfaces.Supplies
{
    //TODO: Change generic types when DTOs are created
    public interface ISupplyService : IService<SupplyDTO, SupplyDTO, SupplyDTO>
    {
    }
}
