using CoC.Business.DTO;
using tpm.dto.admin.Response;
using Core.DTO.Response;
using System;

namespace tpm.business
{
    public interface IOutputFastSaleService : IDisposable
    {
        CRUDResult<OutputFastSaleReadByIDRes> ReadByID(int id, int userID, bool isSearchByCreatedUser);
        PagingResponse<OutputFastSaleSearchRes> Search(int userID, OutputFastSaleSearchReq Obj, bool isSearchByCreatedUser);
        CRUDResult<bool> Cancel(int userID, string Username, string Fullname, int OFSID, string RevokeTypeName);
    }
}
