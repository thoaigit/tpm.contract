using Core.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Lazy<IRepository> _objRepository;
        private readonly Lazy<IReadOnlyRepository> _objReadOnlyRepository;
        private bool _disposedValue;

        public EmployeeService(Lazy<IRepository> objRepository, Lazy<IReadOnlyRepository> objReadOnlyRepository)
        {
            _objRepository = objRepository;
            _objReadOnlyRepository = objReadOnlyRepository;
        }

        public IEnumerable<EmployeeRes> List()
        {
            return ReadAll();
        }


        public IEnumerable<EmployeeRes> ReadAll()
        {
            var result = _objReadOnlyRepository.Value.StoreProcedureQuery<EmployeeRes>("HRM.Employee_ReadAll");
            if (result == null)
            {
                result = new List<EmployeeRes>();
            }
            return result;
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
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EmployeeService()
        {
            Dispose(false);
        }
    }
}
