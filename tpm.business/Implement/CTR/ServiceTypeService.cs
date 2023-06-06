using Core.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IRepository _repository;
        private readonly IReadOnlyRepository _readOnlyRepository;
        private bool _disposedValue;

        public ServiceTypeService(IRepository repository, IReadOnlyRepository readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }

        public IEnumerable<ServiceTypeRes> GetAllServiceTypes()
        {
            var result = _readOnlyRepository.StoreProcedureQuery<ServiceTypeRes>("CTR.ServiceType_ReadAll");
            return result ?? new List<ServiceTypeRes>();
        }

      

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _repository.Dispose();
                    _readOnlyRepository.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServiceTypeService()
        {
            Dispose(false);
        }
    }
}
