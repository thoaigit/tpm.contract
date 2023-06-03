using Autofac.Features.Indexed;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public class PriceService : IPriceService
    {
        private readonly Lazy<IRepository> _objRepository;
        private readonly Lazy<IReadOnlyRepository> _objReadOnlyRepository;
         
        private readonly Lazy<IRepository> _objRepositorySCM;
        private readonly Lazy<IReadOnlyRepository> _objReadOnlyRepositorySCM;

        private bool _disposedValue;
        public PriceService(Lazy<IRepository> objRepository, Lazy<IReadOnlyRepository> objReadOnlyRepository,
            IIndex<string, Lazy<IRepository>> objRepositorySCM, IIndex<string, Lazy<IReadOnlyRepository>> objReadOnlyRepositorySCM)
        {
            _objRepository = objRepository;
            _objReadOnlyRepository = objReadOnlyRepository;
             
            _objRepositorySCM = objRepositorySCM["SCM"];
            _objReadOnlyRepositorySCM = objReadOnlyRepositorySCM["SCM"];
        }
        public CRUDResult<IEnumerable<Price4PartnerRes>> ReadByPIDs(List<int> PIDs)
        {
            if (PIDs == null || PIDs.Count == 0) return new CRUDResult<IEnumerable<Price4PartnerRes>> { StatusCode = CRUDStatusCodeRes.InvalidData, ErrorMessage = "Dữ liệu truyền vào không hợp lệ", Data = null };

            var result = _objReadOnlyRepositorySCM.Value.StoreProcedureQuery<Price4PartnerRes>("SCM.Price_ReadByPIDs4Partner", new
            {
                PIDs = PIDs.ToDataTable<int>().AsTableValuedParameter("[MAT].[IntArray]")
            }).ToList();

            if (result == null || !result.Any()) return new CRUDResult<IEnumerable<Price4PartnerRes>> { StatusCode = CRUDStatusCodeRes.ResourceNotFound };
            return new CRUDResult<IEnumerable<Price4PartnerRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result };
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_objRepository.IsValueCreated)
                        _objRepository.Value.Dispose();

                    if (_objReadOnlyRepository.IsValueCreated)
                        _objReadOnlyRepository.Value.Dispose();
 
                    if (_objRepositorySCM.IsValueCreated)
                        _objRepositorySCM.Value.Dispose();

                    if (_objReadOnlyRepositorySCM.IsValueCreated)
                        _objReadOnlyRepositorySCM.Value.Dispose();

                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~PriceService()
        {
            Dispose(false);
        }
    }
}
