using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.dto.admin;
using tpm.dto.admin.Response;

namespace tpm.business
{
    public interface IServiceTypeService : IDisposable
    {
        IEnumerable<ServiceTypeRes> GetAllServiceTypes();
        
    }
}

