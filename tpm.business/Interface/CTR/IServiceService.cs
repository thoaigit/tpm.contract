using CoC.Business.DTO;
using Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin;
using tpm.dto.admin.MBM;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public interface IServiceService : IDisposable
    {
        CRUDResult<bool> Create(ServiceCreateReq obj);
        IEnumerable<ServiceRes> ReadAll();
        IEnumerable<ServiceRes> GetServicesByTypeId(int serviceTypeId);
        /* CRUDResult<ServiceRes> Update(int service_id, ServiceCreateReq obj);*/
    }
}

