using tpm.dto.admin.Response;
using Core.DTO.Response;
using System;
using System.Collections.Generic;
namespace tpm.business
{
    public interface IPriceService : IDisposable
    {
        CRUDResult<IEnumerable<Price4PartnerRes>> ReadByPIDs(List<int> PIDs);
    }
}
