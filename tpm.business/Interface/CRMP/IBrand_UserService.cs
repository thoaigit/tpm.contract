﻿using tpm.dto.admin.Response;
using Core.DTO.Response;
using System;
using System.Collections.Generic;

namespace tpm.business
{
    public interface IBrand_UserService : IDisposable
    {
        IEnumerable<Brand_UserRes> ReadAll();
        CRUDResult<IEnumerable<Brand_UserRes>> List();
        CRUDResult<Brand_UserRes> ReadByID(int ID);
        CRUDResult<IEnumerable<Brand_UserRes>> ReadByUserID(int UserID);
    }
}
