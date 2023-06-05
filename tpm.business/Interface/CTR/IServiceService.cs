using CoC.Business.DTO;
using Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin.MBM;
using tpm.dto.admin.Response.CTR;

namespace tpm.business
{
    public interface IServiceService : IDisposable
    {
        CRUDResult<ServiceRes> Create(ServiceCreateReq obj);
       /* CRUDResult<ServiceRes> Update(int service_id, ServiceCreateReq obj);*/
    }
}
